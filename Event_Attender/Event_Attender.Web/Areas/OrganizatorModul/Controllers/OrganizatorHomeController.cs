using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Web.ViewModels;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Controllers
{
    [Area("OrganizatorModul")]
    public class OrganizatorHomeController : Controller
    {
        public IActionResult Index()
        {
            using(var ctx= new MojContext())
            {
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
                    DatumOdrzavanja = s.DatumOdrzavanja,
                    VrijemeOdrzavanja = s.VrijemeOdrzavanja,
                    Kategorija = s.Kategorija,
                    OrganizatorNaziv = s.Organizator.Naziv,
                    ProstorOdrzavanjaNaziv = s.ProstorOdrzavanja.Naziv,
                    IsOdobren=s.IsOdobren,
                    IsOtkazan=s.IsOtkazan
                    


                }).Where(g => g.OrganizatorID == 1).ToList();

                ViewData["EventiOrganizatora"] = eventi;
                ViewData["ProstoriOdrzavanja"] = prostoriOdrzavanja;
                return View();
            }
        }

        public IActionResult SnimiEvent(
            string _nazivEventa,
            string _opisEventa,
            string _datumEventa,
            string _vrijemeEventa,
            string _optradio,
            string _optcombo
            )
        {
            int optRadio = Int32.Parse(_optradio);
            int optCombo = Int32.Parse(_optcombo);
            Event e = new Event();
            e.Naziv = _nazivEventa;
            e.Opis = _opisEventa;
            e.DatumOdrzavanja = DateTime.ParseExact(_datumEventa, "yyyy-MM-dd", null);
            e.VrijemeOdrzavanja = _vrijemeEventa;
            e.Kategorija = (Kategorija)(optRadio);
            e.ProstorOdrzavanjaId = optCombo;
            e.IsOdobren = false;
            e.IsOtkazan = false;
            e.OrganizatorId = 1;

            using (MojContext ctx=new MojContext())
            {
                ctx.Event.Add(e);
                ctx.SaveChanges();
            }
            return Redirect("Index");
        }

        public IActionResult EventInfoPrikaz(int EventID)    
        {
            using(var ctx = new MojContext())
            {
                var e = ctx.Event.Where(e => e.Id == EventID)
                    .Include(e => e.Organizator)
                    .Include(e => e.ProstorOdrzavanja)
                    .FirstOrDefault();
                
                var eventInfo = new OrganizatorEventVM {
                   Id=e.Id,
                   Naziv=e.Naziv,
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
                
            }
        }
            
    }

}