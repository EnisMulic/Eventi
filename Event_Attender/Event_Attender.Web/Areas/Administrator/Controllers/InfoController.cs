using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Web.Areas.Administrator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult _EventInfo(int? Id)
        {
            EventVM eventModel = new EventVM();
            if (Id != null)
            {
                MojContext ctx = new MojContext();
                eventModel = ctx.Event
                    .Select
                    (
                        e => new EventVM
                        {
                            Id = e.Id,
                            Naziv = e.Naziv,
                            Opis = e.Opis,
                            DatumOdrzavanja = e.DatumOdrzavanja,
                            VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                            Kategorija = e.Kategorija,
                            IsOdobren = e.IsOdobren,
                            IsOtkazan = e.IsOtkazan,
                            OrganizatorNaziv = e.Organizator.Naziv,
                            AdministratorNaziv = e.AdministratorId != null ? "N/A"
                                                                           : e.Administrator.Osoba.Ime + " " +
                                                                             e.Administrator.Osoba.Prezime,
                            ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                        }
                    )
                    .Where(e => e.Id == Id)
                    .FirstOrDefault();

                ctx.Dispose();
            }
            return View("_EventInfo", eventModel);
        }
    }
}