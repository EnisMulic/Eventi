using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Web.Areas.Client.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eventi.Common;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;
using System.Data;

namespace Eventi.Web.Areas.Client.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Client)]
    [Area("Client")]
    public class ClientController : Controller
    {
        private readonly IEventiApi _eventiApi;

        public ClientController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> Index(string filter)
        {   
            EventSearchVM model = new EventSearchVM();
           
            var user = await HttpContext.GetLoggedInUser();
            if (user != null)
            {
                model.ClientID = user.ID;
            }
           
            if (filter == "Music")
            {
                model.Events = await SearchByCategory(EventCategory.Music);
            }
            else if (filter == "Sport")
            {
                model.Events = await SearchByCategory(EventCategory.Sport);
            }
            else if(filter == "Coulture")
            {
                model.Events = await SearchByCategory(EventCategory.Coulture);
            }
            else if (filter != null)
            {   
                model.Events = await SearchByNameAndLocation(filter);
            }
            else
            {
                model.Events = await GetEvents();
            }

            return View(model);  
        }
        public async Task<List<EventSearchVM.Rows>> GetEvents()
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                IsApproved = true,
                IsCanceled = true,
                Start = DateTime.Now
            });

            return response.Content.Data
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
        public async Task<List<EventSearchVM.Rows>> SearchByCategory(EventCategory category)
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                IsApproved = true,
                IsCanceled = true,
                EventCategory = category,
                Start = DateTime.Now
            });

            return response.Content.Data
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
        public async Task<List<EventSearchVM.Rows>> SearchByNameAndLocation(string filter)
        {
            var response = await _eventiApi.GetEventAsync(new EventSearchRequest()
            {
                Name = filter,
                IsApproved = true,
                IsCanceled = true,
                Start = DateTime.Now
            });

            return response.Content.Data
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


        public async Task<IActionResult> EventDetails(int EventID, int ClientID)
        {
            if (EventID == 0 || ClientID == 0)
            {
                return RedirectToAction("Index");
            }

            var account = await HttpContext.GetLoggedInUser();

            var clientResponse = await _eventiApi.GetClientAsync(new ClientSearchRequest()
            {
                AccountID = account.ID
            });
            var Client = clientResponse.Content.Data.ToList()[0];


            var eventResponse = await _eventiApi.GetEventAsync(EventID);
            var Event = eventResponse.Content;


            EventClientVM model = new EventClientVM {
                EventID = Event.ID,
                Name = Event.Name,
                Category = Event.EventCategory.ToString(),
                Description = Event.Description,
                Start = Event.Start,
                End = Event.End,
                Image = Event.Image,
                ClientID = Client.ID,
                ClientFirstName = Client.FirstName,
                ClientLastName = Client.LastName,
                ClientAddress = Client.Address
               
            };
            
            return View(model);
          
        }


        public async Task<IActionResult> UserDetails() 
        {
            var account = await HttpContext.GetLoggedInUser();
            if (account == null)
            {
                return Redirect("Index");
            }

            var clientResponse = await _eventiApi.GetClientAsync(new ClientSearchRequest()
            {
                AccountID = account.ID
            });
            var client = clientResponse.Content.Data.ToList()[0];


            if (client == null)
            {
                return Redirect("Index");
            }

            ClientDetailsVM model = new ClientDetailsVM
            {
                ID = client.ID,
                Address = client.Address,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Image = client.Image
            };

            var countryResponse = await _eventiApi.GetCountryAsync();

            model.Countries = countryResponse.Content.Data.ToList()
                .Select(i=>new SelectListItem { 
                    Text=i.Name,
                    Value=i.ID.ToString()
                })
                .ToList();
           
            return View(model);     
        }
        
        public async Task<IActionResult> Save(ClientDetailsVM model, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                var response = await _eventiApi.GetCountryAsync();
                model.Countries = response.Content.Data.Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.ID.ToString()
                }).ToList();
                return View(model);
            }

            if (Image != null && Image.Length > 0)
            {
                var nazivSlike = Path.GetFileName(Image.FileName);
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\korisnicke", nazivSlike);
                using (var fajlSteam = new FileStream(putanja, FileMode.Create))
                {
                      await Image.CopyToAsync(fajlSteam);
                }

                model.Image = nazivSlike;
               
            }

            await _eventiApi.UpdateClientAsync(model.ID, new ClientUpdateRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.LastName,
                Email = model.Email,
                CreditCardNumber = model.CreditCardNumber,
                Image = model.Image
            });
            
            return Redirect("UserDetails");
        }
       
        
    }
}


