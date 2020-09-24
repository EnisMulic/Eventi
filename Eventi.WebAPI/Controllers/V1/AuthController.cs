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
        private readonly IAuthService _userAccountService;
        public AuthController(IAuthService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        //[HttpPost(ApiRoutes.UserAccount.Register)]
        //public async Task<IActionResult> Register([FromBody] UserAccountRegistrationRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new AuthFailedResponse
        //        {
        //            Errors = ModelState.Values.SelectMany(i => i.Errors.Select(j => j.ErrorMessage))
        //        });
        //    }

        //    var authResponse = await _userAccountService.RegisterAsync(request);

        //    if (!authResponse.Success)
        //    {
        //        return BadRequest
        //        (
        //            new AuthFailedResponse
        //            {
        //                Errors = authResponse.Errors
        //            }
        //        );
        //    }

        //    return Ok
        //    (
        //        new AuthSuccessResponse
        //        {
        //            Token = authResponse.Token,
        //            RefreshToken = authResponse.RefreshToken
        //        }
        //    );
        //}

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var authResponse = await _userAccountService.LoginAsync(request);

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
            var authResponse = await _userAccountService.RefreshTokenAsync(request);

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
    }
}