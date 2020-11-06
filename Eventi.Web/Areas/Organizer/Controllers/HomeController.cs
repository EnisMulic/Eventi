using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using Eventi.Web.Helper;
using Eventi.Web.Areas.Organizer.ViewModels;
using Eventi.Common;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Web.Areas.Organizer.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Organizer)]
    [Area("Organizer")]
    public class HomeController : Controller
    {
        private readonly IEventiApi _eventiApi;

        public HomeController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        private async Task<List<EventVM>> GetEvents(int orgId)
        {
            var response = await _eventiApi.GetOrganizerEventsAsync(orgId);
            return response.Content.Select
                (
                    i => new EventVM
                    {
                        ID = i.ID,
                        Name = i.Name,
                        Description = i.Description,
                        Image = i.Image,
                        Start = i.Start,
                        End = i.End,
                        EventCategory = i.EventCategory,
                        IsApproved = i.IsApproved,
                        IsCanceled = i.IsCanceled,
                        OrganizerID = i.OrganizerID,
                        VenueID = i.VenueID
                    }
                ).ToList();
            
        }

        public async Task<IActionResult> Index()
        {
            var account = await HttpContext.GetLoggedInUser();
            var response = await _eventiApi.GetOrganizerAsync(new OrganizerSearchRequest()
            {
                AccountID = account.ID
            });

            var organizer = response.Content.Data.ToList()[0];

            List<EventVM> events = await GetEvents(organizer.ID);

            var venueResponse = await _eventiApi.GetVenueAsync();

            ViewData["OrganizatorID"] = organizer.ID;
            ViewData["EventiOrganizatora"] = events;
            ViewData["ProstoriOdrzavanja"] = venueResponse.Content.Data.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Name
            }).ToList(); 

            return View();
        }

        
        

        public async Task<IActionResult> SaveEvent(SaveEventVM data, IFormFile slika)
        {
            if (slika != null && slika.Length > 0)
            {
                var nazivFajla = Path.GetFileName(slika.FileName);
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", nazivFajla);
                using(var fajlSteam = new FileStream(putanja,FileMode.Create))
                {
                    await slika.CopyToAsync(fajlSteam);
                }

                data.Image = nazivFajla;

                int optRadio = Int32.Parse(data.OptRadio);
                int optCombo = Int32.Parse(data.OptCombo);
                var newEvent = new EventInsertRequest()
                {
                    Name = data.Name,
                    Description = data.Description,
                    Image = data.Image,
                    Start = data.Start,
                    End = data.End,
                    EventCategory = (EventCategory)(optRadio),
                    VenueID = optCombo,
                    IsApproved = false,
                    IsCanceled = false,
                    OrganizerID = data.OrganizerID
                };

                await _eventiApi.CreateEventAsync(newEvent);
            }
            return Redirect("Index");
        }

        public async Task<IActionResult> EventInfoPrikaz(int EventID)    
        {
            var response = await _eventiApi.GetEventAsync(EventID);
            var e = response.Content;

            var eventInfo = new EventVM
            {
                ID = e.ID,
                Name = e.Name,
                Image = e.Image,
                Description = e.Description,
                Start = e.Start,
                End = e.End,
                EventCategory = e.EventCategory,
                IsApproved = e.IsApproved,
                IsCanceled = e.IsCanceled,
                OrganizerID = e.OrganizerID,
                VenueID = e.VenueID
            };

            //List<OrganizatorProdajaTipVM> prodajaTipInfo = new List<OrganizatorProdajaTipVM>();
            // prodajaTipInfo = ctx.ProdajaTip.Select(p => new OrganizatorProdajaTipVM
            //{
            //    _tipKarte = p.TipKarte,
            //    _ukupnoKarataTip = p.UkupnoKarataTip.ToString(),
            //    _cijenaTip = p.CijenaTip.ToString(),
            //    _postojeSjedista = p.PostojeSjedista.ToString(),
            //    _brojProdatihKarataTip = p.BrojProdatihKarataTip.ToString(),
            //    _eventID = p.EventId

            //}).Where(e => e._eventID == EventID).ToList();


            //    ViewData["_prodajaTipInfo"] = prodajaTipInfo;
            ViewData["eventInfo"] = eventInfo;
            return View("EventDetails");
        }

        

        public async Task<IActionResult> EventEdit(int EventID)
        {
            var response = await _eventiApi.GetEventAsync(EventID);
            var Event = response.Content;

            var model = new EventEditVM()
            {
                ID = Event.ID,
                Name = Event.Name,
                Description = Event.Description,
                Start = Event.Start,
                End = Event.Start,
                EventCategorySelected = (int)Event.EventCategory
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEventSave(EventEditVM model)
        {
            var response = await _eventiApi.GetEventAsync(model.ID);
            var e = response.Content;

            var request = new EventUpdateRequest()
            {
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                EventCategory = (EventCategory)model.EventCategorySelected
            };

            await _eventiApi.UpdateEventAsync(model.ID, request);

            
            return Redirect("EventInfoPrikaz?EventID=" + model.ID.ToString());

        }

    }

}