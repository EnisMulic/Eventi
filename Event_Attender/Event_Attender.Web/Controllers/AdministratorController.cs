using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventAttender.Data.EF;
using EventAttender.Data.Models;
using Event_Attender.Web.ViewModels;

namespace Event_Attender.Web.Controllers
{
    public class AdministratorController : Controller
    {
        
        public IActionResult Index()
        {
            using (MojContext ctx = new MojContext())
            {
                List<EventInfo> events = ctx.Event
                    .Select
                    (
                        e => new EventInfo
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
                            ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                        }
                    )
                    .Where(e => e.IsOdobren == false)
                    .ToList();

                ViewData["Events"] = events;
            }

            return View();
        }

        public IActionResult Odobri(int Id)
        {
            using(MojContext ctx = new MojContext())
            {
                Event e = ctx.Event.FirstOrDefault(e => e.Id == Id);
                if (e != null) e.IsOdobren = true;
                ctx.SaveChanges();
            }
            return Redirect("/Administrator");
        }

        
  
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   