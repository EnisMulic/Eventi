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
        //EventDisplayVM EventModels = new EventDisplayVM();
        
        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }
        
        public IActionResult _AdminEventDisplay()
        {
            EventDisplayVM EventModels = new EventDisplayVM();

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
            EventDisplayVM EventModels = new EventDisplayVM();
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
            DrzavaDisplayVM DrzavaModels = new DrzavaDisplayVM();
            MojContext ctx = new MojContext();

            DrzavaModels.Drzave = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();

            ctx.Dispose();
            return View(DrzavaModels);
        }

        public IActionResult _DrzavaInfo(int Id)
        {
            DrzavaDisplayVM DrzavaModels = new DrzavaDisplayVM();
            MojContext ctx = new MojContext();

            DrzavaModels.Drzave = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();
            DrzavaModels.OnDisplay = ctx.Drzava
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            ctx.Dispose();
            return View(DrzavaModels);
        }

        public IActionResult _AdminGradDisplay()
        {
            GradDisplayVM GradModels = new GradDisplayVM();
            MojContext ctx = new MojContext();

            GradModels.Gradovi = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .ToList();

            ctx.Dispose();
            return View(GradModels);
        }

        public IActionResult _GradInfo(int Id)
        {
            GradDisplayVM GradModels = new GradDisplayVM();
            MojContext ctx = new MojContext();

            GradModels.Gradovi = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .ToList();
            GradModels.OnDisplay = ctx.Grad
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            ctx.Dispose();
            return View(GradModels);
        }

        public IActionResult _AdminKorisnikDisplay()
        {
            KorisnikDisplayVM KorisnikModels = new KorisnikDisplayVM();
            MojContext ctx = new MojContext();

            KorisnikModels.Korisnici = ctx.Korisnik
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .ToList();

            ctx.Dispose();
            return View(KorisnikModels);
        }

        public IActionResult _KorisnikInfo(int Id)
        {
            KorisnikDisplayVM KorisnikModels = new KorisnikDisplayVM();
            MojContext ctx = new MojContext();

            KorisnikModels.Korisnici = ctx.Korisnik
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .ToList();
            KorisnikModels.OnDisplay = ctx.Korisnik
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
                

            ctx.Dispose();
            return View(KorisnikModels);
        }

        public IActionResult _AdminRadnikDisplay()
        {
            RadnikDisplayVM RadnikModels = new RadnikDisplayVM();
            MojContext ctx = new MojContext();

            RadnikModels.Radnici = ctx.Radnik
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .ToList();

            ctx.Dispose();
            return View(RadnikModels);
        }

        public IActionResult _RadnikInfo(int Id)
        {
            RadnikDisplayVM RadnikModels = new RadnikDisplayVM();
            MojContext ctx = new MojContext();

            RadnikModels.Radnici = ctx.Radnik
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .ToList();
            RadnikModels.OnDisplay = ctx.Radnik
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
                

            ctx.Dispose();
            return View(RadnikModels);
        }

        public IActionResult _AdminOrganizatorDisplay()
        {
            OrganizatorDisplayVM OrganizatorModels = new OrganizatorDisplayVM();
            MojContext ctx = new MojContext();

            OrganizatorModels.Organizatori = ctx.Organizator
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .ToList();
           


            ctx.Dispose();
            return View(OrganizatorModels);
        }

        public IActionResult _OrganizatorInfo(int Id)
        {
            OrganizatorDisplayVM OrganizatorModels = new OrganizatorDisplayVM();
            MojContext ctx = new MojContext();

            OrganizatorModels.Organizatori = ctx.Organizator
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .ToList();
            OrganizatorModels.OnDisplay = ctx.Organizator
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();


            ctx.Dispose();
            return View(OrganizatorModels);
        }

        public IActionResult _AdminIzvodjacDisplay()
        {
            IzvodjacDisplayVM IzvodjacModels = new IzvodjacDisplayVM();
            MojContext ctx = new MojContext();

            IzvodjacModels.Izvodjaci = ctx.Izvodjac
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .ToList();


            ctx.Dispose();
            return View(IzvodjacModels);
        }

        public IActionResult _IzvodjacInfo(int Id)
        {
            IzvodjacDisplayVM IzvodjacModels = new IzvodjacDisplayVM();
            MojContext ctx = new MojContext();

            IzvodjacModels.Izvodjaci = ctx.Izvodjac
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .ToList();
            IzvodjacModels.OnDisplay = ctx.Izvodjac
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();


            ctx.Dispose();
            return View(IzvodjacModels);
        }

        public IActionResult _AdminSponzorDisplay()
        {
            SponzorDisplayVM SponzorModels = new SponzorDisplayVM();
            MojContext ctx = new MojContext();

            SponzorModels.Sponzori = ctx.Sponzor
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .ToList();
            
            ctx.Dispose();
            return View(SponzorModels);
        }

        public IActionResult _SponzorInfo(int Id)
        {
            SponzorDisplayVM SponzorModels = new SponzorDisplayVM();
            MojContext ctx = new MojContext();

            SponzorModels.Sponzori = ctx.Sponzor
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .ToList();
            SponzorModels.OnDisplay = ctx.Sponzor
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();


            ctx.Dispose();
            return View(SponzorModels);
        }

        public IActionResult _AdminProstorDisplay()
        {
            ProstorOdrzavanjaDisplayVM ProstorOdrzavanjaModels = new ProstorOdrzavanjaDisplayVM();
            MojContext ctx = new MojContext();

            ProstorOdrzavanjaModels.Prostori = ctx.ProstorOdrzavanja
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .ToList();
           


            ctx.Dispose();
            return View(ProstorOdrzavanjaModels);
        }

        public IActionResult _ProstorOdrzavanjaInfo(int Id)
        {
            ProstorOdrzavanjaDisplayVM ProstorOdrzavanjaModels = new ProstorOdrzavanjaDisplayVM();
            MojContext ctx = new MojContext();

            ProstorOdrzavanjaModels.Prostori = ctx.ProstorOdrzavanja
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .ToList();
            ProstorOdrzavanjaModels.OnDisplay = ctx.ProstorOdrzavanja
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();


            ctx.Dispose();
            return View(ProstorOdrzavanjaModels);
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