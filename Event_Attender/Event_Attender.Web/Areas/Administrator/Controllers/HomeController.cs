using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Data.Repository;
using Event_Attender.Web.Areas.Administrator.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Autorizacija(korisnik: false, organizator: false, administrator: true, radnik: false)]
    [Area("Administrator")]
    public class HomeController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public HomeController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }

        public IActionResult Index()
        {
            LogPodaci user = HttpContext.GetLogiraniUser();
            AdministratorVM model = new AdministratorVM();
            if (user != null)
            {
                model = uow.AdministratorRepository.GetAll()
                    .Include(i => i.Osoba)
                    .Where(i => i.Osoba.LogPodaciId == user.Id)
                    .Select
                    (
                        i => new AdministratorVM
                        {
                            Id       = i.Id,
                            Ime      = i.Osoba.Ime,
                            Prezime  = i.Osoba.Prezime,
                            Email    = i.Osoba.LogPodaci.Email,
                            Username = i.Osoba.LogPodaci.Username,
                            Password = i.Osoba.LogPodaci.Password,
                            Telefon  = i.Osoba.Telefon,
                            Grad     = i.Osoba.Grad.Naziv
                        }
                    )
                    .SingleOrDefault();
                
            }
            return View(model);
        }

        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }

        #region Drzava
        public IActionResult DrzavaList()
        {
            var model = uow.DrzavaRepository.GetAll()
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult DrzavaUkloni(int Id)
        {
            var item = uow.DrzavaRepository.Get(Id);

            if (item != null)
            {
                uow.DrzavaRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult DrzavaInfo(int Id)
        {
            var model = uow.DrzavaRepository.GetAll()
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

            return View(model);
        }

        public IActionResult DrzavaUredi(int Id)
        {
            var Drzava = uow.DrzavaRepository.Get(Id);
            var model = new DrzavaVM
            {
                Id = Drzava.Id,
                Naziv = Drzava.Naziv
            };
            

            return View(model);
        }

        public IActionResult DrzavaSnimi(DrzavaVM model)
        {
            var item = uow.DrzavaRepository.Get(model.Id);
            item.Naziv = model.Naziv;

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult DrzavaDodaj() => View();

        public IActionResult DrzavaDodajSnimi(DrzavaVM model)
        {
            var item = new Drzava
            {
                Naziv = model.Naziv
            };

            try
            {
                uow.DrzavaRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }
            

            return Redirect("Index");
        }
        #endregion

        #region Grad
        public IActionResult GradList()
        {
            var model = uow.GradRepository.GetAll()
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

            return View(model);
        }

        public IActionResult GradUkloni(int Id)
        {
            var item = uow.GradRepository.Get(Id);

            if (item != null)
            {
                uow.GradRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult GradInfo(int Id)
        {
            var Grad = uow.GradRepository.Get(Id);
            var model = new GradVM
            {
                Id = Grad.Id,
                Naziv = Grad.Naziv,
                DrzavaId = Grad.DrzavaId,
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradUredi(int Id)
        {
            var Grad = uow.GradRepository.Get(Id);
            var model = new GradVM
            {
                Id = Grad.Id,
                Naziv = Grad.Naziv,
                DrzavaId = Grad.DrzavaId,
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradSnimi(GradVM model)
        {
            var item = uow.GradRepository.Get(model.Id);
            item.Naziv = model.Naziv;
            item.DrzavaId = model.DrzavaId;
            

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult GradDodaj()
        {
            var model = new GradVM
            {
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradDodajSnimi(GradVM model)
        {
            var item = new Grad
            {
                Naziv = model.Naziv,
                DrzavaId = model.DrzavaId
            };

            try
            {
                uow.GradRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }

       
        #endregion

        #region Sponzor
        public IActionResult SponzorList()
        {
            var model = uow.SponzorRepository.GetAll()
                .Select
                (
                    i => new SponzorVM
                    {
                        Id      = i.Id,
                        Naziv   = i.Naziv,
                        Telefon = i.Telefon,
                        Email   = i.Email
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult SponzorUkloni(int Id)
        {
            var item = uow.SponzorRepository.Get(Id);

            if (item != null)
            {
                uow.SponzorRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult SponzorInfo(int Id)
        {
            var i = uow.SponzorRepository.Get(Id);
            var model = new SponzorVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                Telefon = i.Telefon,
                Email = i.Email
            };

            return View(model);
        }

        public IActionResult SponzorUredi(int Id)
        {
            var i = uow.SponzorRepository.Get(Id);
            var model = new SponzorVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                Telefon = i.Telefon,
                Email = i.Email
            };

            return View(model);
        }

        public IActionResult SponzorSnimi(SponzorVM model)
        {
            var item = uow.SponzorRepository.Get(model.Id);
            item.Naziv = model.Naziv;
            item.Telefon = model.Telefon;
            item.Email = model.Email;

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult SponzorDodaj() => View();

        public IActionResult SponzorDodajSnimi(SponzorVM model)
        {
            var item = new Sponzor
            {
                Naziv = model.Naziv,
                Telefon = model.Telefon,
                Email = model.Email
            };

            try
            {
                uow.SponzorRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        #region Korisnik
        public IActionResult KorisnikList()
        {
            var model = uow.KorisnikRepository.GetAll()
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Username = i.Osoba.LogPodaci.Username,
                        Email = i.Osoba.LogPodaci.Email,
                        Telefon = i.Osoba.Telefon,
                        Adresa = i.Adresa,
                        GradId = i.Osoba.Grad.Id,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        PostanskiBroj = i.PostanskiBroj
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult KorisnikUkloni(int Id)
        {
            var item = uow.KorisnikRepository.Get(Id);

            if (item != null)
            {
                uow.KorisnikRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult KorisnikInfo(int Id)
        {
            var model = uow.KorisnikRepository.GetAll()
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradId = i.Osoba.Grad.Id,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        Username = i.Osoba.LogPodaci.Username,
                        Email = i.Osoba.LogPodaci.Email,
                        Password = i.Osoba.LogPodaci.Password,
                        Adresa = i.Adresa

                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult KorisnikUredi(int Id)
        {
            var model = uow.KorisnikRepository.GetAll()
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradId = i.Osoba.Grad.Id,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        Username = i.Osoba.LogPodaci.Username,
                        Email = i.Osoba.LogPodaci.Email,
                        Password = i.Osoba.LogPodaci.Password,
                        Adresa = i.Adresa

                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();
            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);
        }

        public IActionResult KorisnikSnimi(KorisnikVM model)
        {
            var item = uow.KorisnikRepository.GetAll()
                .Include(i => i.Osoba)
                    .ThenInclude(i => i.LogPodaci)
                .Where(i => i.Id == model.Id)
                .SingleOrDefault();

            item.Osoba.Ime = model.Ime;
            item.Osoba.Prezime = model.Prezime;
            item.Osoba.LogPodaci.Username = model.Username;
            item.Osoba.LogPodaci.Email = model.Email;
            item.Osoba.LogPodaci.Password = model.Password;
            item.Osoba.GradId = model.GradId;
            item.Osoba.Telefon = model.Telefon;
            item.Adresa = model.Adresa;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult KorisnikDodaj()
        {
            var model = new KorisnikVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult KorisnikDodajSnimi(KorisnikVM model)
        {
            var log = new LogPodaci
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            uow.LogPodaciRepository.Add(log);

            var o = new Osoba
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Telefon = model.Telefon,
                GradId = model.GradId,
                LogPodaci = log
            };

            uow.OsobaRepository.Add(o);

            var item = new Korisnik
            {
                Osoba = o
            };

            try
            {
                uow.KorisnikRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        #region Radnik
        public IActionResult RadnikList()
        {
            var model = uow.RadnikRepository.GetAll()
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Username = i.Osoba.LogPodaci.Username,
                        Telefon = i.Osoba.Telefon,
                        GradNaziv = i.Osoba.Grad.Naziv
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult RadnikUkloni(int Id)
        {
            var item = uow.RadnikRepository.Get(Id);

            if (item != null)
            {
                uow.RadnikRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult RadnikInfo(int Id)
        {
            var model = uow.RadnikRepository.GetAll()
                 .Select
                 (
                     i => new RadnikVM
                     {
                         Id = i.Id,
                         Ime = i.Osoba.Ime,
                         Prezime = i.Osoba.Prezime,
                         Telefon = i.Osoba.Telefon,
                         GradId = i.Osoba.Grad.Id,
                         GradNaziv = i.Osoba.Grad.Naziv,
                         Username = i.Osoba.LogPodaci.Username,
                         Email = i.Osoba.LogPodaci.Email,
                         Password = i.Osoba.LogPodaci.Password
                     }
                 )
                 .Where(i => i.Id == Id)
                 .FirstOrDefault();

            return View(model);
        }

        public IActionResult RadnikUredi(int Id)
        {
            var model = uow.RadnikRepository.GetAll()
                .Select
                (
                    i => new RadnikVM
                    {
                        Id = i.Id,
                        Ime = i.Osoba.Ime,
                        Prezime = i.Osoba.Prezime,
                        Telefon = i.Osoba.Telefon,
                        GradId = i.Osoba.Grad.Id,
                        GradNaziv = i.Osoba.Grad.Naziv,
                        Username = i.Osoba.LogPodaci.Username,
                        Email = i.Osoba.LogPodaci.Email,
                        Password = i.Osoba.LogPodaci.Password
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();
            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();


            return View(model);
        }

        public IActionResult RadnikSnimi(RadnikVM model)
        {
            var item = uow.RadnikRepository.GetAll()
                .Include(i => i.Osoba)
                    .ThenInclude(i => i.LogPodaci)
                .Where(i => i.Id == model.Id)
                .FirstOrDefault();
                
            item.Osoba.Ime = model.Ime;
            item.Osoba.Prezime = model.Prezime;
            item.Osoba.LogPodaci.Username = model.Username;
            item.Osoba.LogPodaci.Email = model.Email;
            item.Osoba.LogPodaci.Password = model.Password;
            item.Osoba.GradId = model.GradId;
            item.Osoba.Telefon = model.Telefon;
            

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult RadnikDodaj()
        {
            var model = new RadnikVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult RadnikDodajSnimi(RadnikVM model)
        {
            var log = new LogPodaci
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            uow.LogPodaciRepository.Add(log);

            var o = new Osoba
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Telefon = model.Telefon,
                GradId = model.GradId,
                LogPodaci = log
            };

            uow.OsobaRepository.Add(o);

            var item = new Radnik
            {
                Osoba = o
            };

            try
            {
                uow.RadnikRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        #region Organizator
        public IActionResult OrganizatorList()
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult OrganizatorUkloni(int Id)
        {
            var item = uow.OrganizatorRepository.Get(Id);
            
            if (item != null)
            {

                uow.OrganizatorRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult OrganizatorInfo(int Id)
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email,
                        Password = i.LogPodaci.Password
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult OrganizatorUredi(int Id)
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email,
                        Password = i.LogPodaci.Password
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);

            
        }

        public IActionResult OrganizatorSnimi(OrganizatorVM model)
        {
            var item = uow.OrganizatorRepository.GetAll()
                .Include(i => i.LogPodaci)
                .Where(i => i.Id == model.Id)
                .FirstOrDefault();

            if(item.LogPodaciId == null)
            {
                item.LogPodaci = new LogPodaci();
            }

            item.Naziv = model.Naziv;
            item.LogPodaci.Username = model.Username;
            item.LogPodaci.Email = model.Email;
            item.LogPodaci.Password = model.Password;
            item.GradId = model.GradId;
            item.Telefon = model.Telefon;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult OrganizatorDodaj()
        {
            var model = new OrganizatorVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        

        public IActionResult OrganizatorDodajSnimi(OrganizatorVM model)
        {
            
                var log = new LogPodaci
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                uow.LogPodaciRepository.Add(log);


                var item = new Organizator
                {
                    Naziv = model.Naziv,
                    Telefon = model.Telefon,
                    GradId = model.GradId,
                    LogPodaci = log
                };

                try
                {
                    uow.OrganizatorRepository.Add(item);
                }
                catch //(Exception e)
                {
                    //Console.WriteLine("{0} Exception caught.", e);
                }


                
            
            return Redirect("Index");
        }
        #endregion

        #region Izvodjac
        public IActionResult IzvodjacList()
        {
            var model = uow.IzvodjacRepository.GetAll()
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

            return View(model);
        }

        public IActionResult IzvodjacUkloni(int Id)
        {
            var item = uow.IzvodjacRepository.Get(Id);

            if (item != null)
            {
                uow.IzvodjacRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult IzvodjacInfo(int Id)
        {
            var i = uow.IzvodjacRepository.Get(Id);
            var model = new IzvodjacVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                TipIzvodjaca = i.TipIzvodjaca
            };

            return View(model);
        }

        public IActionResult IzvodjacUredi(int Id)
        {
            var i = uow.IzvodjacRepository.Get(Id);
            var model = new IzvodjacVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                TipIzvodjaca = i.TipIzvodjaca
            };


            return View(model);
        }

        public IActionResult IzvodjacSnimi(IzvodjacVM model)
        {
            var item = uow.IzvodjacRepository.Get(model.Id);
    

            item.Naziv = model.Naziv;
            item.TipIzvodjaca = model.TipIzvodjaca;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult IzvodjacDodaj()
        {
            var model = new IzvodjacVM();
            
            return View(model);
        }

        public IActionResult IzvodjacDodajSnimi(IzvodjacVM model)
        {
            var item = new Izvodjac
            {
                Naziv = model.Naziv,
                TipIzvodjaca = model.TipIzvodjaca
            };

            try
            {
                uow.IzvodjacRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        #region Prostor
        public IActionResult ProstorList()
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult ProstorUkloni(int Id)
        {
            var item = uow.ProstorOdrzavanjaRepository.Get(Id);

            if (item != null)
            {
                uow.ProstorOdrzavanjaRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult ProstorInfo(int Id)
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult ProstorUredi(int Id)
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();
            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);
        }

        public IActionResult ProstorSnimi(ProstorOdrzavanjaVM model)
        {
            var item = uow.ProstorOdrzavanjaRepository.Get(model.Id);


            item.Naziv = model.Naziv;
            item.Adresa = model.Adresa;
            item.TipProstoraOdrzavanja = model.TipProstoraOdrzavanja;
            item.GradId = model.GradId;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult ProstorDodaj()
        {
            var model = new ProstorOdrzavanjaVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };
            return View(model);
        }

        public IActionResult ProstorDodajSnimi(ProstorOdrzavanjaVM model)
        {
            var item = new ProstorOdrzavanja
            {
                Naziv = model.Naziv,
                Adresa = model.Adresa,
                TipProstoraOdrzavanja = model.TipProstoraOdrzavanja,
                GradId = model.GradId
            };

            try
            {
                uow.ProstorOdrzavanjaRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        #region Event
        public IActionResult EventList()
        {
            var model = uow.EventRepository.GetAll()
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
                        Slika = e.Slika,
                        OrganizatorNaziv = e.Organizator.Naziv,
                        AdministratorIme = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Ime,
                        AdministratorPrezime = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Prezime,
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                    }
                 )
                .ToList();

            return View(model);
        }

        public IActionResult EventUkloni(int Id)
        {
            var item = uow.EventRepository.Get(Id);

            if (item != null)
            {
                uow.EventRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult EventInfo(int Id)
        {
            var model = uow.EventRepository.GetAll()
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
                        Slika = e.Slika,
                        IsOdobren = e.IsOdobren,
                        IsOtkazan = e.IsOtkazan,
                        OrganizatorNaziv = e.Organizator.Naziv,
                        AdministratorIme = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Ime,
                        AdministratorPrezime = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Prezime,
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            return View(model);
        }

        
        public async Task<IActionResult> EventSnimi(EventVM model, IFormFile slika)
        {
            var item = ctx.Event.Find(model.Id);


            String fajlNaziv = new String("");
            if (slika != null && slika.Length > 0)
            {
                fajlNaziv = Path.GetFileName(slika.FileName);
                //var mappedPath = HttpContext.GetServerVariable.MapPath("~/Content/Images/");
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", fajlNaziv);
                using (var fajlSteam = new FileStream(putanja, FileMode.Create))
                {
                    await slika.CopyToAsync(fajlSteam);
                }
            }


            item.Naziv               = model.Naziv;
            item.Kategorija          = model.Kategorija;
            item.Opis                = model.Opis;
            item.OrganizatorId       = model.OrganizatorId;
            item.ProstorOdrzavanjaId = model.ProstorOdrzavanjaId;
            item.DatumOdrzavanja     = model.DatumOdrzavanja;
            item.VrijemeOdrzavanja   = model.VrijemeOdrzavanja;
            item.IsOdobren           = model.IsOdobren;
            item.IsOtkazan           = model.IsOtkazan;

            if(fajlNaziv != "")
                item.Slika = fajlNaziv;

            if (model.AdministratorId != 0)
                item.AdministratorId = model.AdministratorId;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult EventUredi(int Id)
        {
            var model = uow.EventRepository.GetAll()
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
                        Slika = e.Slika,
                        IsOdobren = e.IsOdobren,
                        IsOtkazan = e.IsOtkazan,
                        OrganizatorNaziv = e.Organizator.Naziv,
                        AdministratorIme = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Ime,
                        AdministratorPrezime = e.AdministratorId != null ? "N/A" : e.Administrator.Osoba.Prezime,
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            model.Organizatori = uow.OrganizatorRepository.GetAll().Select(
                d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList();
            model.Administratori = uow.AdministratorRepository.GetAll().Select(
                d => new SelectListItem(d.Osoba.Ime + " " + d.Osoba.Prezime, d.Id.ToString())).ToList();
            model.Prostori = uow.ProstorOdrzavanjaRepository.GetAll().Select(
                d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList();



            return View(model);
        }

        public IActionResult EventDodaj()
        {
            var model = new EventVM
            {
                Organizatori = uow.OrganizatorRepository.GetAll().Select(
                    d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList(),
                Administratori = uow.AdministratorRepository.GetAll().Select(
                    d => new SelectListItem(d.Osoba.Ime + " " + d.Osoba.Prezime, d.Id.ToString())).ToList(),
                Prostori = uow.ProstorOdrzavanjaRepository.GetAll().Select(
                    d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> EventDodajSnimi(EventVM model, IFormFile slika)
        {
            String fajlNaziv = new String("");
            if (slika != null && slika.Length > 0)
            {
                fajlNaziv = Path.GetFileName(slika.FileName);
                //var mappedPath = HttpContext.GetServerVariable.MapPath("~/Content/Images/");
                var putanja = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", fajlNaziv);
                using (var fajlSteam = new FileStream(putanja, FileMode.Create))
                {
                    await slika.CopyToAsync(fajlSteam);
                }
            }
            
            var item = new Event
            {
                Naziv               = model.Naziv,
                Kategorija          = model.Kategorija,
                Opis                = model.Opis,
                OrganizatorId       = model.OrganizatorId,
                ProstorOdrzavanjaId = model.ProstorOdrzavanjaId,
                DatumOdrzavanja     = model.DatumOdrzavanja,
                VrijemeOdrzavanja   = model.VrijemeOdrzavanja,
                IsOdobren           = model.IsOdobren,
                IsOtkazan           = model.IsOtkazan,
                Slika               = fajlNaziv != "" ? fajlNaziv : null 
            };

            
            if (model.AdministratorId != 0)
                item.AdministratorId = model.AdministratorId;

            try
            {
                ctx.Event.Add(item);
                await ctx.SaveChangesAsync();
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
        #endregion

        //public IActionResult EmailPostoji(string Email)
        //{
        //    var email = uow.LogPodaciRepository.GetAll()
        //        .SingleOrDefault(i => i.Email == Email);

        //    return Json(email != null);
        //}

        public IActionResult DodajRadnikaNaEvent(int id)
        {
            var Event = uow.EventRepository.GetAll()
                .Include(i => i.ProstorOdrzavanja)
                .Where(i => i.Id == id)
                .SingleOrDefault();


            var model = new DodajRadnikEventVM
            {
                EventId = Event.Id,
                EventNaziv = Event.Naziv,
                Datum = Event.DatumOdrzavanja.ToString("dd/MM/yyyy"),
                Vrijeme = Event.VrijemeOdrzavanja,
                ProstorOdrzavanja = Event.ProstorOdrzavanja.Naziv,
                Radnici = uow.RadnikRepository.GetAll()
                    .Select
                    (
                        i => new SelectListItem
                        {
                            Value = i.Id.ToString(),
                            Text = i.Osoba.Ime + " " + i.Osoba.Prezime
                        }
                    )
                    .ToList()
            };

            return PartialView(model);
        }

        public IActionResult SnimiRadnikEvent(DodajRadnikEventVM model)
        {
            var RadnikoviEventi = uow.RadnikEventRepository.GetAll()
                .Where
                (
                    i => i.RadnikId == model.RadnikId && 
                         i.EventId == model.EventId
                )
                .ToList();

            if(!RadnikoviEventi.Any())
            {
                var RadnikEvent = new RadnikEvent
                {
                    EventId = model.EventId,
                    RadnikId = model.RadnikId
                };
                uow.RadnikEventRepository.Add(RadnikEvent);
            }

            return Redirect("/Administrator/Home/Index");
        }

    }

    
}