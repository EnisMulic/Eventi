using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Eventi.Contracts.V1.Requests;
using Eventi.Core.Interfaces;
using Eventi.Core.Settings;
using Eventi.Database;
using Eventi.Domain;
using AutoMapper;
using Eventi.Core.Helpers;

namespace Eventi.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly EventiContext _context;
        private readonly IMapper _mapper;

        public AuthService
        (
            JwtSettings jwtSettings, 
            TokenValidationParameters tokenValidationParameters,
            EventiContext context,
            IMapper mapper
        )
        {
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginRequest request)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .Where(i => i.Username == request.Username)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            var hash = HashHelper.GenerateHash(account.PasswordSalt, request.Password);
            if (hash != account.PasswordHash)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }


            return await GenerateAuthenticationResultForAccountAsync(account);

        }

        public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var validatedToken = GetPrincipalFromToken(request.Token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
            }

            storedRefreshToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var account = await _context.RefreshTokens
                .AsNoTracking()
                .Include(i => i.Account)
                .Where(i => i.Token == request.Token)
                .Select(i => i.Account)
                .SingleOrDefaultAsync();

            return await GenerateAuthenticationResultForAccountAsync(account);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        
        private async Task<AuthenticationResult> GenerateAuthenticationResultForAccountAsync(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim("id", account.ID.ToString())
            };

            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                AccountID = account.ID,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthenticationResult> RegisterClientAsync(ClientRegistrationRequest request)
        {
            var result = await CheckUniqueAccount(request);
            if (result.Errors != null)
            {
                return result;
            }

            var account = await CreateAccountAsync(request);

            var person = await CreatePersonAsync(request, account);

            var client = _mapper.Map<Client>(request);
            client.PersonID = person.ID;
            _context.Clients.Add(client);
            _context.SaveChanges();

            return await GenerateAuthenticationResultForAccountAsync(account);
        }

        public async Task<AuthenticationResult> RegisterAdministratorAsync(AdministratorRegistrationRequest request)
        {
            var result = await CheckUniqueAccount(request);
            if (result.Errors != null)
            {
                return result;
            }

            var account = await CreateAccountAsync(request);

            var person = await CreatePersonAsync(request, account);

            var admin = _mapper.Map<Administrator>(request);
            admin.PersonID = person.ID;
            _context.Administrators.Add(admin);
            _context.SaveChanges();

            return await GenerateAuthenticationResultForAccountAsync(account);
        }

        public async Task<AuthenticationResult> RegisterOrganizerAsync(OrganizerRegistrationRequest request)
        {
            var result = await CheckUniqueAccount(request);
            if (result.Errors != null)
            {
                return result;
            }

            var account = await CreateAccountAsync(request);

            var organizer = _mapper.Map<Organizer>(request);
            organizer.AccountID = account.ID;
            _context.Organizers.Add(organizer);
            _context.SaveChanges();

            return await GenerateAuthenticationResultForAccountAsync(account);
        }

        private async Task<Account> CreateAccountAsync(RegistrationRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.PasswordSalt = HashHelper.GenerateSalt();
            account.PasswordHash = HashHelper.GenerateHash(account.PasswordSalt, request.Password);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private async Task<Person> CreatePersonAsync(RegistrationRequest request, Account account)
        {
            var person = _mapper.Map<Person>(request);
            person.AccountID = account.ID;
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private async Task<AuthenticationResult> CheckUniqueAccount(RegistrationRequest request)
        {
            var result = new AuthenticationResult();

            var existingEmail = await FindByEmailAsync(request.Email);
            
            if (existingEmail != null)
            {
                result.Errors.Append("User with this email address already exists");
            }

            var existingUsername = await FindByUsernameAsync(request.Username);

            if (existingUsername != null)
            {
                result.Errors.Append("User with this username already exists");
            }

            return result;
        }

        public async Task<Account> FindByEmailAsync(string Email)
        {
            return await _context.Accounts
                .AsNoTracking()
                .Where(i => i.Email == Email)
                .SingleOrDefaultAsync();
        }

        public async Task<Account> FindByUsernameAsync(string Username)
        {
            return await _context.Accounts
                .AsNoTracking()
                .Where(i => i.Username == Username)
                .SingleOrDefaultAsync();
        }
    }
}
