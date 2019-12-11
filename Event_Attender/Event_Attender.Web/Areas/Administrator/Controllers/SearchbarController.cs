using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.Administrator.Models;
using Microsoft.EntityFrameworkCore;

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

        #region Event: ToDo: Uredi, Spasi
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

        public IActionResult ObrisiEvent(int Id)
        {
            MojContext ctx = new MojContext();

            Event item = ctx.Event.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminEventDisplay");
        }

        public IActionResult UrediEvent(int Id)
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
            return View("_EventForma", EventModels);
        }

        public IActionResult SnimiEvent
        (
            string Id, 
            string Naziv, 
            string Opis,
            string Datum, 
            string Vrijeme,
            string Kategorija,
            string IsOdobren,
            string IsOtkatan,
            string Prostor,
            string Organizator,
            string Administrator)
        {
            MojContext ctx = new MojContext();
            Event item = ctx.Event.Find(int.Parse(Id));
            item.Naziv = Naziv;
            item.Opis = Opis;
            item.DatumOdrzavanja = DateTime.ParseExact(Datum, "yyyy-MM-dd", null);
            item.VrijemeOdrzavanja = Vrijeme;
            item.Kategorija = (Kategorija)Enum.Parse(typeof(Kategorija), Kategorija);
            item.IsOdobren = IsOdobren != null ? true : false;
            item.IsOtkazan = IsOtkatan != null ? true : false;

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminEventDisplay");
        }

        #endregion
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

        #region Drzava:
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

        public IActionResult ObrisiDrzava(int Id)
        {
            MojContext ctx = new MojContext();

            Drzava item = ctx.Drzava.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminDrzavaDisplay");
        }

        public IActionResult UrediDrzava(int Id)
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
            return View("_DrzavaForma", DrzavaModels);
        }

        public IActionResult SnimiDrzava(string Id, string Naziv)
        {
            MojContext ctx = new MojContext();
            Drzava item = ctx.Drzava.Find(int.Parse(Id));
            item.Naziv = Naziv;

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminDrzavaDisplay");
        }

        #endregion

        #region Grad: ToDo: SnimiGrad - Drzava
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

        public IActionResult ObrisiGrad(int Id)
        {
            MojContext ctx = new MojContext();

            Grad item = ctx.Grad.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminGradDisplay");
        }

        public IActionResult UrediGrad(int Id)
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
            return View("_GradForma", GradModels);
        }

        public IActionResult SnimiGrad(string Id, string Naziv, string Drzava)
        {
            MojContext ctx = new MojContext();
            Grad item = ctx.Grad.Find(int.Parse(Id));
            item.Naziv = Naziv;
            //implement Drzava

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminGradDisplay");
        }

        #endregion

        #region Korisnik: ToDo: Snimi - Grad
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

        public IActionResult ObrisiKorisnik(int Id)
        {
            MojContext ctx = new MojContext();

            Korisnik item = ctx.Korisnik.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminKorisnikDisplay");
        }

        public IActionResult UrediKorisnik(int Id)
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
            return View("_KorisnikForma", KorisnikModels);
        }

        public IActionResult SnimiKorisnik
        (
            string Id,
            string Ime,
            string Prezime,
            string Telefon,
            string Adresa,
            string PostanskiBroj,
            string Grad
        )
        {
            MojContext ctx = new MojContext();
            Korisnik item = ctx.Korisnik
                .Include(i => i.Osoba)
                .Where(i => i.Id == int.Parse(Id))
                .FirstOrDefault();
                
            item.Osoba.Ime = Ime;
            item.Osoba.Prezime = Prezime;
            item.Osoba.Telefon = Telefon;
            item.Adresa = Adresa;
            item.PostanskiBroj = PostanskiBroj;

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminKorisnikDisplay");
        }

        #endregion

        #region Radnik: ToDo: Snimi - Grad
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

        public IActionResult ObrisRadnik(int Id)
        {
            MojContext ctx = new MojContext();

            Radnik item = ctx.Radnik.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminRadnikDisplay");
        }

        public IActionResult UrediRadnik(int Id)
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
            return View("_RadnikForma", RadnikModels);
        }

        public IActionResult SnimiRadnik(string Id, string Ime, string Prezime, string Telefon, string Grad)
        {
            MojContext ctx = new MojContext();
            Radnik item = ctx.Radnik
                .Include(i => i.Osoba)
                .Where(i => i.Id == int.Parse(Id))
                .FirstOrDefault();

            item.Osoba.Ime = Ime;
            item.Osoba.Prezime = Prezime;
            item.Osoba.Telefon = Telefon;
            
            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminKorisnikDisplay");
        }

        #endregion

        #region Organizator: ToDo: Snimi - Grad
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

        public IActionResult ObrisiOrganizator(int Id)
        {
            MojContext ctx = new MojContext();

            Organizator item = ctx.Organizator.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminOrganizatorDisplay");
        }

        public IActionResult UrediOrganizator(int Id)
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
            return View("_OrganizatorForma", OrganizatorModels);
        }

        public IActionResult SnimiOrganizator(string Id, string Naziv, string Telefon, string Grad)
        {
            MojContext ctx = new MojContext();
            Organizator item = ctx.Organizator.Find(int.Parse(Id));
            item.Naziv = Naziv;
            item.Telefon = Telefon;
            //imprement Grad

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminDrzavaDisplay");
        }

        #endregion

        #region Izvodjac
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

        public IActionResult ObrisiIzvodjac(int Id)
        {
            MojContext ctx = new MojContext();

            Izvodjac item = ctx.Izvodjac.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminIzvodjacDisplay");
        }

        public IActionResult UrediIzvodjac(int Id)
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
            return View("_IzvodjacForma", IzvodjacModels);
        }

        public IActionResult SnimiIzvodjac(string Id, string Naziv, string Tip)
        {
            MojContext ctx = new MojContext();
            Izvodjac item = ctx.Izvodjac.Find(int.Parse(Id));
            item.Naziv = Naziv;
            item.TipIzvodjaca = (TipIzvodjaca)Enum.Parse(typeof(TipIzvodjaca), Tip);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminIzvodjacDisplay");
        }

        #endregion

        #region Sponzor:

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

        public IActionResult ObrisiSponzor(int Id)
        {
            MojContext ctx = new MojContext();

            Sponzor item = ctx.Sponzor.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminSponzorDisplay");
        }

        public IActionResult UrediSponzor(int Id)
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
            return View("_SponzorForma", SponzorModels);
        }

        public IActionResult SnimiSponzor(string Id, string Naziv, string Telefon, string Email)
        {
            MojContext ctx = new MojContext();
            Sponzor item = ctx.Sponzor.Find(int.Parse(Id));
            item.Naziv = Naziv;
            item.Telefon = Telefon;
            item.Email = Email;

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminSponzorDisplay");
        }

        #endregion

        #region Prostor: ToDo: Snimi - Grad
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

        public IActionResult ObrisiProstorOdrzavanja(int Id)
        {
            MojContext ctx = new MojContext();

            ProstorOdrzavanja item = ctx.ProstorOdrzavanja.Find(Id);

            if (item != null)
            {
                ctx.Remove(item);

                ctx.SaveChanges();
            }

            ctx.Dispose();

            return Redirect("_AdminProstorDisplay");
        }

        public IActionResult UrediProstorOdrzavanja(int Id) 
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
            return View("_ProstorOdrzavanjaForma", ProstorOdrzavanjaModels);

        }

        public IActionResult SnimiProstorOdrzavanja(string Id, string Naziv, string Adresa, string Grad, string Tip) 
        {
            MojContext ctx = new MojContext();
            ProstorOdrzavanja item = ctx.ProstorOdrzavanja.Find(int.Parse(Id));
            item.Naziv = Naziv;
            item.Adresa = Adresa;
            item.TipProstoraOdrzavanja = (TipProstoraOdrzavanja)Enum.Parse(typeof(TipProstoraOdrzavanja), Tip);

            ctx.SaveChanges();
            ctx.Dispose();

            return Redirect("_AdminProstorDisplay");
        }
        #endregion
    }
}