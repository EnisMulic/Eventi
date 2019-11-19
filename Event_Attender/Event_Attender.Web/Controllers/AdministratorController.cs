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
            Test_HardCode_DodajEvent(3);
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

        public void Test_HardCode_DodajEvent(int n)
        {
            
            using MojContext ctx = new MojContext();
            if (ctx.Organizator.Count() == 0)
            {
                List<Organizator> organizatori = new List<Organizator>
                    {
                        new Organizator { Naziv = "Organizator 1"},
                        new Organizator { Naziv = "Organizator 2"},
                        new Organizator { Naziv = "Organizator 3"}
                    };
                ctx.Organizator.AddRange(organizatori);
                ctx.SaveChanges();
            }

            if (ctx.ProstorOdrzavanja.Count() == 0)
            {
                if (ctx.Grad.Count() == 0)
                {
                    ctx.Drzava.Add(new Drzava { Naziv = "Burkina Faso" });
                    ctx.SaveChanges();

                    Drzava tempDrzava = ctx.Drzava
                        .Where(d => d.Naziv == "Burkina Faso")
                        .FirstOrDefault();
                    ctx.Grad.Add(new Grad { Naziv = "Ouagadougou", DrzavaId = tempDrzava.Id});
                    ctx.SaveChanges();
                }

                Grad tempGrad = ctx.Grad.FirstOrDefault();
                List<ProstorOdrzavanja> prostoriOdrzavanja = new List<ProstorOdrzavanja>
                    {
                        new ProstorOdrzavanja {Naziv = "Prostor 1", GradId = tempGrad.Id},
                        new ProstorOdrzavanja {Naziv = "Prostor 2", GradId = tempGrad.Id}
                    };
                ctx.ProstorOdrzavanja.AddRange(prostoriOdrzavanja);
                ctx.SaveChanges();
            }


            if (ctx.Event.Where(e => e.IsOdobren == false).Count() == 0)
            {
                List<Event> events = new List<Event>();
                
                List<Organizator> organizatori = ctx.Organizator.ToList();
                Random rndOrg = new Random();

                List<ProstorOdrzavanja> prostori = ctx.ProstorOdrzavanja.ToList();
                Random rndProstor = new Random();

                Random rndEvent = new Random();
                for (int i = 0; i < n; i++)
                {
                    
                    events.Add
                    (
                        new Event
                        {
                            Naziv = "Event Test " + rndEvent.Next(),
                            DatumOdrzavanja = Convert.ToDateTime("31.12.2019"),
                            VrijemeOdrzavanja = "20:00",
                            Kategorija = (Kategorija)(i % 2 + 1),
                            IsOdobren = false,
                            IsOtkazan = false,
                            OrganizatorId = organizatori[rndOrg.Next(0, organizatori.Count() - 1)].Id,
                            ProstorOdrzavanjaId = prostori[rndProstor.Next(0, prostori.Count() - 1)].Id
                        }
                    );
                }
                ctx.AddRange(events);

                ctx.SaveChanges();
            }
        }
  
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   