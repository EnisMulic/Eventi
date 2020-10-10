using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventi.Web.ViewModels;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;
using System.IdentityModel.Tokens.Jwt;
using Eventi.Contracts.V1.Responses;

namespace Eventi.Web.Controllers
{
    public class LoginController : Controller
    {
        private MojContext ctx;
        private readonly IAuthApi _authApi;
        private readonly IEventiApi _eventiApi;

        public LoginController(MojContext context, IAuthApi authApi, IEventiApi eventiApi)
        {
            ctx = context;
            _authApi = authApi;
            _eventiApi = eventiApi;
        }
        public IActionResult Index()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }

        public async Task<IActionResult> Login(LoginVM input)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", input);
            }

            var account = await _authApi.LoginAsync(new LoginRequest()
            {
                Username = input.Username,
                Password = input.Password
            });

            if(account.Content != null)
            {
                var jwt = account.Content.Token;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                var role = token.Claims.First(claim => claim.Type == "Role").Value;
                var accountID = Convert.ToInt32(token.Claims.First(claim => claim.Type == "id").Value);

                switch (role)
                {
                    case "Client":
                        return await HandleClientLogin(accountID);
                    case "Administrator":
                        return await HandleAdministratorLogin(accountID);
                    case "Organizer":
                        return await HandleOrganizerLogin(accountID);
                }
            }
         
            TempData["error_message"] = "Niste unijeli ispravne podatke za prijavu";
            //return RedirectToAction("Index");

            return Redirect("/ModulKorisnik/Korisnik/Index");

        }

        private async Task<IActionResult> HandleClientLogin(int accountID)
        {
            var response = await _eventiApi.GetClientAsync(new ClientSearchRequest() { AccountID = accountID });
            var client = response.Content.Data.ToList()[0];

            HttpContext.SetLoggedInUser(new AccountResponse() 
            {
                ID = client.ID,
                Username = client.Username,
                Email = client.Email
            });

            return Redirect("/ModulKorisnik/Korisnik/Index");
        }

        private async Task<IActionResult> HandleAdministratorLogin(int accountID)
        {
            var response = await _eventiApi.GetAdministratorAsync(new AdministratorSearchRequest() { AccountID = accountID });
            var administrator = response.Content.Data.ToList()[0];

            HttpContext.SetLoggedInUser(new AccountResponse()
            {
                ID = administrator.ID,
                Username = administrator.Username,
                Email = administrator.Email
            });

            return Redirect("/Administrator/Home/Index");
        }

        private async Task<IActionResult> HandleOrganizerLogin(int accountID)
        {
            var response = await _eventiApi.GetOrganizerAsync(new OrganizerSearchRequest() { AccountID = accountID });
            var organizer = response.Content.Data.ToList()[0];

            HttpContext.SetLoggedInUser(new AccountResponse()
            {
                ID = organizer.ID,
                Username = organizer.Username,
                Email = organizer.Email
            });

            return Redirect("/OrganizatorModul/OrganizatorHome/Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.RemoveCookie(); 
            return RedirectToAction("Index", "Home","");
        }
    }
}