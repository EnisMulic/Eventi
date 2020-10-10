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
using AutoMapper;
using Eventi.Web.Models;
using Newtonsoft.Json.Linq;

namespace Eventi.Web.Controllers
{
    public class LoginController : Controller
    {
        private MojContext ctx;
        private readonly IAuthApi _authApi;
        private readonly IEventiApi _eventiApi;
        private readonly IMapper _mapper;

        public LoginController(MojContext context, IAuthApi authApi, IEventiApi eventiApi, IMapper mapper)
        {
            ctx = context;
            _authApi = authApi;
            _eventiApi = eventiApi;
            _mapper = mapper;
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

            var response = await _authApi.LoginAsync(new LoginRequest()
            {
                Username = input.Username,
                Password = input.Password
            });


            if(response.Content != null)
            {
                var jwt = response.Content.Token;
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


            TempData["error_message"] = response.Error.Content;

            return Redirect("/Index");

        }

        private async Task<IActionResult> HandleClientLogin(int accountID)
        {
            var response = await _eventiApi.GetClientAsync(new ClientSearchRequest() { AccountID = accountID });
            var client = response.Content.Data.ToList()[0];

            var account = _mapper.Map<Account>(client);

            HttpContext.SetLoggedInUser(account);

            return Redirect("/Client/Korisnik/Index");
        }

        private async Task<IActionResult> HandleAdministratorLogin(int accountID)
        {
            var response = await _eventiApi.GetAdministratorAsync(new AdministratorSearchRequest() { AccountID = accountID });
            var administrator = response.Content.Data.ToList()[0];

            var account = _mapper.Map<Account>(administrator);
            HttpContext.SetLoggedInUser(account);

            return Redirect("/Administrator/Home/Index");
        }

        private async Task<IActionResult> HandleOrganizerLogin(int accountID)
        {
            var response = await _eventiApi.GetOrganizerAsync(new OrganizerSearchRequest() { AccountID = accountID });
            var organizer = response.Content.Data.ToList()[0];

            var account = _mapper.Map<Account>(organizer);
            HttpContext.SetLoggedInUser(account);

            return Redirect("/Organizer/OrganizatorHome/Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.RemoveCookie(); 
            return RedirectToAction("Index", "Home","");
        }
    }
}