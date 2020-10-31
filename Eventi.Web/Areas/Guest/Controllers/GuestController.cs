using System;
using System.Collections.Generic;
using System.Linq;
using Eventi.Web.Areas.Guest.Models;
using Eventi.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eventi.Sdk;
using System.Threading.Tasks;
using AspNetCore;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Web.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class GuestController : Controller
    {
        private readonly IEventiApi _eventiApi;
        private readonly IAuthApi _authApi;

        public GuestController(IEventiApi eventiApi, IAuthApi authApi)
        {
            _eventiApi = eventiApi;
            _authApi = authApi;
        }
        public async Task<IActionResult> SearchByName(string filter)
        {
            EventSearchVM model = new EventSearchVM();
           
            if (filter != null)
            {
                var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
                {
                    Name = filter,
                    IsApproved = true,
                    IsCanceled = true,
                    Start = DateTime.Now
                });

                model.Events = response.Content.Data
                    .Select
                    (
                        i => new EventSearchVM.Rows()
                        {
                            EventID = i.ID,
                            Name = i.Name,
                            Category = i.EventCategory.ToString(),
                            Start = i.Start,
                            End = i.End,
                            Image = i.Image
                        }
                    ).ToList();
            }
       
            return View(model);  
        }
        public async Task<IActionResult> SearchByLocation(string lokacija)
        {
            // To do: Implement event search by venue name
            EventSearchVM model = new EventSearchVM();

            if (lokacija != null)
            {
                var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
                {
                    Name = lokacija,
                    IsApproved = true,
                    IsCanceled = true,
                    Start = DateTime.Now
                });

                model.Events = response.Content.Data
                    .Select
                    (
                        i => new EventSearchVM.Rows()
                        {
                            EventID = i.ID,
                            Name = i.Name,
                            Category = i.EventCategory.ToString(),
                            Start = i.Start,
                            End = i.End,
                            Image = i.Image
                        }
                    ).ToList();
            }

            return View(model);
        }
        public async Task<IActionResult> Registration()
        {
            var countriesResponse = await _eventiApi.GetCountryAsync();
            var citiesResponse = await _eventiApi.GetCityAsync();

            var countries = countriesResponse.Content.Data
                .Select
                (
                    i => new SelectListItem()
                    {
                        Value = i.ID.ToString(),
                        Text = i.Name
                    }
                )
                .ToList();

            var cities = citiesResponse.Content.Data
                .Select
                (
                    i => new SelectListItem()
                    {
                        Value = i.ID.ToString(),
                        Text = i.Name
                    }
                )
                .ToList();

            RegistrationVM model = new RegistrationVM()
            {
                Cities = cities,
                Countries = countries
            };

            return View(model);
        }

        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            if (!ModelState.IsValid)
            {
                var response = await _eventiApi.GetCountryAsync();
                model.Countries = response.Content.Data
                    .Select(i => new SelectListItem(i.Name, i.ID.ToString()))
                    .ToList();

                return View("Registration", model);
            }

            var registration = new ClientRegistrationRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.LastName,
                CreditCardNumber = model.CreditCardNumber,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username
            };


            await _authApi.ClientRegisterAsync(registration);
            return Redirect("/Login/Index");
        }
    }
}
