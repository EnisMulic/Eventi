using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.Administrator.Models;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class SearchbarController : Controller
    {
        //private readonly MojContext _context;

        //public SearchbarController(MojContext context)
        //{
        //    _context = context;
        //}
        EventDisplayVM EventModels = new EventDisplayVM();

        EventVM GetEventVM(Event e)
        {
            return new EventVM()
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
            };
        }

        List<EventVM> GetListEventVM()
        {
            MojContext ctx = new MojContext();
            List<EventVM> events = ctx.Event
                .Select(e => GetEventVM(e))
                .ToList();

            ctx.Dispose();
            return events;
        }
        
        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }
        public IActionResult _AdminEventDisplay()
        {
            
            MojContext ctx = new MojContext();
            List<EventVM> events = ctx.Event
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
                .ToList();

            ctx.Dispose();
            EventModels.Events = events;
            return View(EventModels);
        }

        public IActionResult _EventInfo(int Id)
        {
            MojContext ctx = new MojContext();

            EventModels.Events = ctx.Event
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
                .ToList();
            EventModels.OnDisplay = ctx.Event
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
            return View(EventModels);
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
            using (MojContext ctx = new MojContext())
            {
                List<Drzava> Drzave = ctx.Drzava.ToList();
                ViewData["Drzave"] = Drzave;
            }
            //List<Drzava> Drzave = _context.Drzava.ToList();
            //ViewData["Drzave"] = Drzave;

            return View();
        }

        public IActionResult _AdminGradDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Grad> Gradovi = ctx.Grad
                    //.Include(d => d.Drzava)
                    .ToList();
                ViewData["Gradovi"] = Gradovi;
            }
            //var Gradovi = _context.Grad.ToList();
            //ViewData["Gradovi"] = Gradovi;
            return View();
        }

        public IActionResult _AdminKorisnikDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Korisnik> Korisnici = ctx.Korisnik
                    //.Include(o => o.Osoba)
                    .ToList();
                ViewData["Korisnici"] = Korisnici;
            }
            //List<Korisnik> Korisnici = _context.Korisnik.ToList();
            //ViewData["Korisnici"] = Korisnici;
            return View();
        }

        public IActionResult _AdminRadnikDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Radnik> Radnici = ctx.Radnik
                    //.Include(o => o.Osoba)
                    .ToList();
                ViewData["Radnici"] = Radnici;
            }
            //List<Radnik> Radnici = _context.Radnik.ToList();
            //ViewData["Radnici"] = Radnici;
            return View();
        }

        public IActionResult _AdminOrganizatorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Organizator> Organizatori = ctx.Organizator.ToList();
                ViewData["Organizatori"] = Organizatori;
            }
            //List<Organizator> Organizatori = _context.Organizator.ToList();
            //ViewData["Organizatori"] = Organizatori;
            return View();
        }

        public IActionResult _AdminIzvodjacDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Izvodjac> Izvodjaci = ctx.Izvodjac.ToList();
                ViewData["Izvodjaci"] = Izvodjaci;
            }
            //List<Izvodjac> Izvodjaci = _context.Izvodjac.ToList();
            //ViewData["Izvodjaci"] = Izvodjaci;
            return View();
        }

        public IActionResult _AdminSponzorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<Sponzor> Sponzori = ctx.Sponzor.ToList();
                ViewData["Sponzori"] = Sponzori;
            }
            //List<Sponzor> Sponzori = _context.Sponzor.ToList();
            //ViewData["Sponzori"] = Sponzori;
            return View();
        }

        public IActionResult _AdminProstorDisplay()
        {
            using (MojContext ctx = new MojContext())
            {
                List<ProstorOdrzavanja> ProstoriOdrzavanja = ctx.ProstorOdrzavanja.ToList();
                ViewData["ProstoriOdrzavanja"] = ProstoriOdrzavanja;
            }
            //List<ProstorOdrzavanja> ProstoriOdrzavanja = _context.ProstorOdrzavanja.ToList();
            //ViewData["ProstoriOdrzavanja"] = ProstoriOdrzavanja;
            return View();
        }

        public IActionResult _DrzavaForma(int Id)
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

        
    }
}