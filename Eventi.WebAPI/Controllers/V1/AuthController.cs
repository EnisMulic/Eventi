using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eventi.Contracts.V1;
using Eventi.Contracts.V1.Requests;
using Eventi.Contracts.V1.Responses;
using Eventi.Core.Interfaces;

namespace Eventi.WebAPI.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.Auth.RegisterClient)]
        public async Task<IActionResult> RegisterClient([FromBody] ClientRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(i => i.Errors.Select(j => j.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterClientAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.RegisterAdministrator)]
        public async Task<IActionResult> RegisterAdministrator([FromBody] AdministratorRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(i => i.Errors.Select(j => j.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterAdministratorAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.RegisterOrganizer)]
        public async Task<IActionResult> RegisterOrganizer([FromBody] OrganizerRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(i => i.Errors.Select(j => j.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterOrganizerAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var authResponse = await _authService.LoginAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _authService.RefreshTokenAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest
                (
                    new AuthFailedResponse
                    {
                        Errors = authResponse.Errors
                    }
                );
            }

            return Ok
            (
                new AuthSuccessResponse
                {
                    Token = authResponse.Token,
                    RefreshToken = authResponse.RefreshToken
                }
            );
        }

        [HttpGet(ApiRoutes.Auth.Get)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _authService.GetAsync(id);

            if(response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

    }
}