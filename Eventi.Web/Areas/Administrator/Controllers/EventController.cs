using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class EventController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public EventController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
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


            item.Naziv = model.Naziv;
            item.Kategorija = model.Kategorija;
            item.Opis = model.Opis;
            item.OrganizatorId = model.OrganizatorId;
            item.ProstorOdrzavanjaId = model.ProstorOdrzavanjaId;
            item.DatumOdrzavanja = model.DatumOdrzavanja;
            item.VrijemeOdrzavanja = model.VrijemeOdrzavanja;
            item.IsOdobren = model.IsOdobren;
            item.IsOtkazan = model.IsOtkazan;

            if (fajlNaziv != "")
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
                Naziv = model.Naziv,
                Kategorija = model.Kategorija,
                Opis = model.Opis,
                OrganizatorId = model.OrganizatorId,
                ProstorOdrzavanjaId = model.ProstorOdrzavanjaId,
                DatumOdrzavanja = model.DatumOdrzavanja,
                VrijemeOdrzavanja = model.VrijemeOdrzavanja,
                IsOdobren = model.IsOdobren,
                IsOtkazan = model.IsOtkazan,
                Slika = fajlNaziv != "" ? fajlNaziv : null
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
    }
}
