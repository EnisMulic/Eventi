using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Controllers
{
    [Area("Administrator")]
    public class AdministratorController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult _AdminEventDisplay()
        {
            //Test_HardCode_DodajEvent(3);
            using (MojContext ctx = new MojContext())
            {
                List<Event> events = ctx.Event
                    .Include(o => o.Organizator)
                    .Include(p => p.ProstorOdrzavanja)
                    .ToList();

                ViewData["Events"] = events;
            }

            return View();
        }

        public IActionResult Odobri(int Id)
        {
            using (MojContext ctx = new MojContext())
            {
                Event e = ctx.Event.FirstOrDefault(e => e.Id == Id);
                if (e != null) e.IsOdobren = true;
                ctx.SaveChanges();
            }
            return Redirect("/Administrator");
        }

        public IActionResult _AdminDrzavaDisplay()
        {
            using(MojContext ctx = new MojContext())
            {
                List<Drzava> Drzave = ctx.Drzava.ToList();
                ViewData["Drzave"] = Drzave;
            }
            return View();
        }

        public IActionResult _AdminGradDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Grad> Gradovi = ctx.Grad
                    .Include(d => d.Drzava)
                    .ToList();
                ViewData["Gradovi"] = Gradovi;
            }
            return View();
        }

        public IActionResult _AdminKorisnikDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Korisnik> Korisnici = ctx.Korisnik
                    .Include(o => o.Osoba)
                    .ToList();
                ViewData["Korisnici"] = Korisnici;
            }
            return View();
        }

        public IActionResult _AdminRadnikDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Radnik> Radnici = ctx.Radnik
                    .Include(o => o.Osoba)
                    .ToList();
                ViewData["Radnici"] = Radnici;
            }
            return View();
        }

        public IActionResult _AdminOrganizatorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Organizator> Organizatori = ctx.Organizator.ToList();
                ViewData["Organizatori"] = Organizatori;
            }
            return View();
        }

        public IActionResult _AdminIzvodjacDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Izvodjac> Izvodjaci = ctx.Izvodjac.ToList();
                ViewData["Izvodjaci"] = Izvodjaci;
            }
            return View();
        }

        public IActionResult _AdminSponzorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Sponzor> Sponzori = ctx.Sponzor.ToList();
                ViewData["Sponzori"] = Sponzori;
            }
            return View();
        }

        public IActionResult _AdminProstorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<ProstorOdrzavanja> ProstoriOdrzavanja = ctx.ProstorOdrzavanja.ToList();
                ViewData["ProstoriOdrzavanja"] = ProstoriOdrzavanja;
            }
            return View();
        }

        public IActionResult _DrzavaForma(int? Id)
        {
            Drzava drzava;
            using (MojContext ctx = new MojContext())
            {
                
                if (Id != null)
                {
                    drzava = ctx.Drzava.Find(Id);
                }
                else
                {
                    drzava = ctx.Drzava.FirstOrDefault();
                }
            }
            
            return View(drzava);
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
                            DatumOdrzavanja = DateTime.ParseExact("31.12.2019", "dd.MM.yyyy", null),
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