using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Web.ViewModels;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Http;
using Event_Attender.Web.Helper;

namespace Event_Attender.Web.Controllers
{
    [Autorizacija(korisnik: false, organizator: true, administrator: false, radnik: false)]
    [Area("OrganizatorModul")]
    public class OrganizatorHomeController : Controller
    {
        
        private readonly MojContext ctx;

        public OrganizatorHomeController(MojContext context)
        {
            ctx = context;
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

        public IActionResult Index()
        {
            Organizator org = new Organizator();
            LogPodaci l = HttpContext.GetLogiraniUser();
            if (l != null)
            {
                 org = ctx.Organizator.Where(o => o.LogPodaciId == l.Id).SingleOrDefault();
                
            }

            List<OrganizatorEventVM> eventi = getListuEvenata(org.Id);

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

    }

}