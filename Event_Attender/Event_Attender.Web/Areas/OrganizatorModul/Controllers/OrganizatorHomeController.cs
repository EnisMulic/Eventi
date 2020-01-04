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

namespace Event_Attender.Web.Controllers
{
    [Area("OrganizatorModul")]
    public class OrganizatorHomeController : Controller
    {
        private readonly MojContext ctx;

        public OrganizatorHomeController(MojContext context)
        {
            ctx = context;
        }

        public IActionResult Index()
        {
            //using(var ctx= new MojContext())
            //{
                List<ProstorOdrzavanjaVM> prostoriOdrzavanja = ctx.ProstorOdrzavanja.Select(s => new ProstorOdrzavanjaVM
                {
                    ProstorOdrzavanjaID = s.Id,
                    Naziv = s.Naziv
                }).ToList();

                List<OrganizatorEventVM> eventi = ctx.Event.Select(s => new OrganizatorEventVM
                {
                    Id = s.Id,
                    OrganizatorID = s.OrganizatorId,
                    Naziv = s.Naziv,
                    Opis = s.Opis,
                    Slika=s.Slika,
                    DatumOdrzavanja = s.DatumOdrzavanja,
                    VrijemeOdrzavanja = s.VrijemeOdrzavanja,
                    Kategorija = s.Kategorija,
                    OrganizatorNaziv = s.Organizator.Naziv,
                    ProstorOdrzavanjaNaziv = s.ProstorOdrzavanja.Naziv,
                    IsOdobren=s.IsOdobren,
                    IsOtkazan=s.IsOtkazan
                }).Where(g => g.OrganizatorID == 1 && g.DatumOdrzavanja > DateTime.Today).ToList();

                ViewData["EventiOrganizatora"] = eventi;
                ViewData["ProstoriOdrzavanja"] = ctx.ProstorOdrzavanja.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Naziv
                }).ToList(); ;
                return View();
            //}
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
                    OrganizatorId = 1
                };

                //using (MojContext ctx=new MojContext())
                //{
                ctx.Event.Add(e);
                await ctx.SaveChangesAsync();
                //  }
            }
            return Redirect("Index");
        }

        public IActionResult EventInfoPrikaz(int EventID)    
        {
            //using(var ctx = new MojContext())
            //{
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

                ViewData["eventInfo"] = eventInfo;
                return View("EventInfo");
                
           // }
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
            
            return Redirect("Index");
        }
            
    }

}