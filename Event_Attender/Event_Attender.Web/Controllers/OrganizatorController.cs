using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Web.ViewModels;
using EventAttender.Data.EF;
using EventAttender.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Controllers
{
    public class OrganizatorController : Controller
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

                List<EventInfo> eventi = ctx.Event.Select(s => new EventInfo
                {
                    Id = s.Id,
                    OrganizatorID = s.OrganizatorId,
                    Naziv = s.Naziv,
                    Opis = s.Opis,
                    DatumOdrzavanja = s.DatumOdrzavanja,
                    VrijemeOdrzavanja = s.VrijemeOdrzavanja,
                    Kategorija = s.Kategorija,
                    OrganizatorNaziv = s.Organizator.Naziv,
                    ProstorOdrzavanjaNaziv = s.ProstorOdrzavanja.Naziv


                }).Where(g => g.OrganizatorID == 1).ToList();

                ViewData["EventiOrganizatora"] = eventi;
                ViewData["ProstoriOdrzavanja"] = prostoriOdrzavanja;
                return View("Index");
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
    }

}