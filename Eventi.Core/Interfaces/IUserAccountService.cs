using System.Threading.Tasks;
using Eventi.Contracts.V1.Requests;
using Eventi.Domain;

namespace Eventi.Core.Interfaces
{
    public interface IUserAccountService
    {
        Task<AuthenticationResult> RegisterAsync(AccountRegistrationRequest request);

        Task<AuthenticationResult> AuthenticateAsync(AccountAuthenticationRequest request);

        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
