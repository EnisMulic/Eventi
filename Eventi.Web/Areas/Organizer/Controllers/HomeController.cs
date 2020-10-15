using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using Eventi.Web.Helper;
using Eventi.Web.Areas.Organizer.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Nexmo.Api;
using Eventi.Common;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Web.Areas.Organizer.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Organizer)]
    [Area("Organizer")]
    public class HomeController : Controller
    {
        private readonly MojContext ctx;
        private readonly IConfiguration _configuration;
        private readonly IEventiApi _eventiApi;

        public HomeController(MojContext context, IConfiguration configuration, IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
            ctx = context;
            _configuration = configuration;
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

            var model = new StatistickiPodaciVM
            {
                Redovi = ctx.Event.Where(x => x.OrganizatorId == organizer.ID).Select(x => new StatistickiPodaciVM.Rows
                {
                    NazivEventa = x.Naziv,
                    UkupnoBrojProdatihKarata = ctx.ProdajaTip.Where(y => y.EventId == x.Id).Select(y => y.BrojProdatihKarataTip).Sum(),
                    UkupanPrihodPoEventu = ctx.ProdajaTip.Where(y => y.EventId == x.Id).Sum(y => y.BrojProdatihKarataTip * y.CijenaTip),
                    ProsjecnaOcjenaEventa = ctx.Recenzija.Where(y => y.Kupovina.EventId == x.Id).Select(y => y.Ocjena).Average().ToString("F") 
                   }).ToList()
            };

            var venueResponse = await _eventiApi.GetVenueAsync();

            ViewData["StatistickiPodaci"] = model;
            ViewData["OrganizatorID"] = organizer.ID;
            ViewData["EventiOrganizatora"] = events;
            ViewData["ProstoriOdrzavanja"] = venueResponse.Content.Data.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.Name
            }).ToList(); 

            return View();
        }

        bool provjeraTipKarte(int eventID,TipKarte tip)
        {
            var provjeraTip = ctx.ProdajaTip.Where(p => p.EventId == eventID).Include(e => e.Event).ToList();

            if (provjeraTip.Count() == 0)
            {
                return true;
            }
            else
            {
                foreach (var x in provjeraTip)
                {
                    if (x.TipKarte == tip)
                        return false;
                }
            }

            return true;
        }

        public IActionResult SnimiProdajaTip(SnimiProdajaTipVM data)
        {
            int optCombo = Int32.Parse(data._tipKarteCombo);
                       
            if (provjeraTipKarte(data._eventID,(TipKarte)optCombo))
            {
                int optRadio = data._postojeSjedista;
                ProdajaTip p = new ProdajaTip
                {
                    TipKarte = (TipKarte)optCombo,
                    UkupnoKarataTip = data._ukupnoKarataTip,
                    PostojeSjedista = optRadio != 0,
                    CijenaTip = data._cijenaTip,
                    BrojProdatihKarataTip=0,
                    EventId = data._eventID,
                };

                ctx.ProdajaTip.Add(p);
                ctx.SaveChanges();
            }

            return Redirect("EventInfoPrikaz?EventID=" + data._eventID.ToString());
        }

        public async Task<IActionResult> SaveEvent(SnimiEventVM data, IFormFile slika)
        {
            if (slika != null && slika.Length > 0)
            {
                var nazivFajla = Path.GetFileName(slika.FileName);
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", nazivFajla);
                using(var fajlSteam = new FileStream(putanja,FileMode.Create))
                {
                    await slika.CopyToAsync(fajlSteam);
                }

                data._slika = nazivFajla;

                int optRadio = Int32.Parse(data._optradio);
                int optCombo = Int32.Parse(data._optcombo);
                Event e = new Event
                {
                    Naziv = data._nazivEventa,
                    Opis = data._opisEventa,
                    Slika = data._slika,
                    DatumOdrzavanja = DateTime.ParseExact(data._datumEventa, "yyyy-MM-dd", null),
                    VrijemeOdrzavanja = data._vrijemeEventa,
                    Kategorija = (Kategorija)(optRadio),
                    ProstorOdrzavanjaId = optCombo,
                    IsOdobren = false,
                    IsOtkazan = false,
                    OrganizatorId = data._organizatorID
                };

                ctx.Event.Add(e);
                await ctx.SaveChangesAsync();

                var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("Eventi@outlook.com", "Event Attender team");
                List<EmailAddress> tos = new List<EmailAddress>
                {
                    new EmailAddress("zinedin.mezit.98@gmail.com","Zinedin Mezit")
                };
                var subject = "Obavijest o novom eventu";
                var htmlContent = $"<h1>Kreiran je novi event : {data._nazivEventa}</h1>";
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
                var response = await client.SendEmailAsync(msg);
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

        public IActionResult OtkaziEvent(int EventID)
        {
            var query =
                from ev in ctx.Event
                where ev.Id == EventID
                select ev;
            foreach(var _event in query)
            {
                _event.IsOtkazan = true;
            }


            var Nexmo_Api_Key = _configuration.GetSection("NEXMO_API_KEY").Value;
            var Nexmo_Api_Secret = _configuration.GetSection("NEXMO_API_SECRET").Value;

            var client = new Nexmo.Api.Client(creds: new Nexmo.Api.Request.Credentials(nexmoApiKey: Nexmo_Api_Key, nexmoApiSecret: Nexmo_Api_Secret));
                client.SMS.Send(new SMS.SMSRequest
                {
                    from = "NEXMO_Zinedin",
                    to = "387111111111",
                    text = "Ovo je test poruka preko Nexmo"
                });

            return Redirect("EventInfoPrikaz?EventID=" + EventID.ToString());
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