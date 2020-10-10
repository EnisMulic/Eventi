using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Web.ViewModels;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using Eventi.Web.Helper;
using Eventi.Web.Areas.OrganizatorModul.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Nexmo.Api;
using Eventi.Common;

namespace Eventi.Web.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Organizer)]
    [Area("Organizer")]
    public class OrganizatorHomeController : Controller
    {
        private readonly MojContext ctx;
        private readonly IConfiguration _configuration;

        public OrganizatorHomeController(MojContext context,IConfiguration configuration)
        {
            ctx = context;
            _configuration = configuration;
        }

        List<OrganizatorEventVM> getListuEvenata(int orgId)
        {
            return ctx.Event.Select(s => new OrganizatorEventVM
            {
                Id = s.Id,
                OrganizatorID = s.OrganizatorId,
                Naziv = s.Naziv,
                Opis = s.Opis,
                Slika = s.Slika,
                DatumOdrzavanja = s.DatumOdrzavanja,
                VrijemeOdrzavanja = s.VrijemeOdrzavanja,
                Kategorija = s.Kategorija,
                OrganizatorNaziv = s.Organizator.Naziv,
                ProstorOdrzavanjaNaziv = s.ProstorOdrzavanja.Naziv,
                IsOdobren = s.IsOdobren,
                IsOtkazan = s.IsOtkazan
            }).Where(g => g.OrganizatorID == orgId && g.DatumOdrzavanja > DateTime.Today).ToList();
        }

        public async Task<IActionResult> Index()
        {
            Organizator org = new Organizator();
            var l = await HttpContext.GetLoggedInUser();
            //if (l != null)
            //{
            //     org = ctx.Organizator.Where(o => o.LogPodaciId == l.ID).SingleOrDefault();
                
            //}

            List<OrganizatorEventVM> eventi = getListuEvenata(org.Id);

            var model = new StatistickiPodaciVM
            {
                Redovi = ctx.Event.Where(x => x.OrganizatorId == org.Id).Select(x => new StatistickiPodaciVM.Rows
                {
                    NazivEventa = x.Naziv,
                    UkupnoBrojProdatihKarata = ctx.ProdajaTip.Where(y => y.EventId == x.Id).Select(y => y.BrojProdatihKarataTip).Sum(),
                    UkupanPrihodPoEventu = ctx.ProdajaTip.Where(y => y.EventId == x.Id).Sum(y => y.BrojProdatihKarataTip * y.CijenaTip),
                    ProsjecnaOcjenaEventa = ctx.Recenzija.Where(y => y.Kupovina.EventId == x.Id).Select(y => y.Ocjena).Average().ToString("F") 
                   }).ToList()
            };

            ViewData["StatistickiPodaci"] = model;
            ViewData["OrganizatorID"] = org.Id;
                ViewData["EventiOrganizatora"] = eventi;
                ViewData["ProstoriOdrzavanja"] = ctx.ProstorOdrzavanja.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
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

        public async Task<IActionResult> SnimiEvent(SnimiEventVM data,IFormFile slika)
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

        public IActionResult EventInfoPrikaz(int EventID)    
        {
                var e = ctx.Event.Where(e => e.Id == EventID)
                    .Include(e => e.Organizator)
                    .Include(e => e.ProstorOdrzavanja)
                    .FirstOrDefault();

                var eventInfo = new OrganizatorEventVM {
                   Id=e.Id,
                   Naziv=e.Naziv,
                   Slika=e.Slika,
                   Opis=e.Opis,
                   DatumOdrzavanja=new DateTime(e.DatumOdrzavanja.Year,e.DatumOdrzavanja.Month,e.DatumOdrzavanja.Day),
                   VrijemeOdrzavanja=e.VrijemeOdrzavanja,
                   Kategorija=e.Kategorija,
                   IsOdobren=e.IsOdobren,
                   IsOtkazan=e.IsOtkazan,
                   OrganizatorNaziv=e.Organizator.Naziv,
                   OrganizatorID=e.OrganizatorId,
                   ProstorOdrzavanjaNaziv=e.ProstorOdrzavanja.Naziv
               };

            List<OrganizatorProdajaTipVM> prodajaTipInfo = new List<OrganizatorProdajaTipVM>();
             prodajaTipInfo = ctx.ProdajaTip.Select(p => new OrganizatorProdajaTipVM
            {
                _tipKarte = p.TipKarte,
                _ukupnoKarataTip = p.UkupnoKarataTip.ToString(),
                _cijenaTip = p.CijenaTip.ToString(),
                _postojeSjedista = p.PostojeSjedista.ToString(),
                _brojProdatihKarataTip = p.BrojProdatihKarataTip.ToString(),
                _eventID = p.EventId

            }).Where(e => e._eventID == EventID).ToList();


                ViewData["_prodajaTipInfo"] = prodajaTipInfo;
                ViewData["eventInfo"] = eventInfo;
                return View("EventInfo");                
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

            ctx.SaveChanges();

            var Nexmo_Api_Key = _configuration.GetSection("NEXMO_API_KEY").Value;
            var Nexmo_Api_Secret = _configuration.GetSection("NEXMO_API_SECRET").Value;

            var client = new Client(creds: new Nexmo.Api.Request.Credentials(nexmoApiKey: Nexmo_Api_Key, nexmoApiSecret: Nexmo_Api_Secret));
                client.SMS.Send(new SMS.SMSRequest
                {
                    from = "NEXMO_Zinedin",
                    to = "387603022082",
                    text = "Ovo je test poruka preko Nexmo"
                });

            return Redirect("EventInfoPrikaz?EventID=" + EventID.ToString());
        }

        public IActionResult OdobriEvent(int EventID)
        {
            var query =
                from ev in ctx.Event
                where ev.Id == EventID
                select ev;
            foreach (var _event in query)
            {
                _event.IsOtkazan = false;
            }

            ctx.SaveChanges();

            return Redirect("EventInfoPrikaz?EventID=" + EventID.ToString());
        }

        public IActionResult UrediEvent(int EventId)
        {
            var _event = ctx.Event.Where(x => x.Id == EventId).FirstOrDefault();

            var model = new EventUrediFormVM
            {
                EventId = _event.Id,
                Sponzori = ctx.Sponzor.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList(),
                NazivEventaOd =_event.Naziv,
                OpisEventaOd = _event.Opis,
                VrijemeOdrzavanjaOd = _event.VrijemeOdrzavanja
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UrediSnimiEvent(EventUrediFormVM model)
        {
            
            var _event = ctx.Event.Where(x => x.Id == model.EventId).FirstOrDefault();
            _event.Naziv = model.NazivEventaOd;
            _event.Opis = model.OpisEventaOd;
            _event.VrijemeOdrzavanja = model.VrijemeOdrzavanjaOd;
            if(model.SponzorOdabir != 0)
            {
                var noviSponzor = new SponzorEvent
                {
                    EventId = model.EventId,
                    SponzorId = model.SponzorOdabir,
                    Prioritet = Prioritet.Silver

                };

                ctx.SponzorEvent.Add(noviSponzor);
            }

            ctx.SaveChanges();
            return Redirect("EventInfoPrikaz?EventID=" + model.EventId.ToString());

        }

    }

}