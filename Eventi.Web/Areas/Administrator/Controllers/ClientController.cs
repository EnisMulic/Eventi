using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class ClientController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        private readonly IEventiApi _eventiApi;
        public ClientController(MojContext context, IEventiApi eventiApi)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> ClientList()
        {
            var response = await _eventiApi.GetClientAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new KorisnikVM
                    {
                        Id = i.ID,
                        Ime = i.FirstName,
                        Prezime = i.LastName,
                        Username = i.Username,
                        Email = i.Email,
                        Telefon = i.PhoneNumber,
                        Adresa = i.Address
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> ClientRemove(int Id)
        {
            await _eventiApi.DeleteClientAsync(Id); 
            return Redirect("Index");
        }

        public IActionResult ClientInfo(int Id)
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

        public IActionResult ClientEdit(int Id)
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
                        Adresa = i.Adresa,
                        PostanskiBroj = i.PostanskiBroj,
                        BrojKreditneKartice = i.BrojKreditneKartice,
                        LogPodaciId = i.Osoba.LogPodaci.Id

                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();
            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);
        }

        public IActionResult ClientSave(KorisnikVM model)
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
            item.PostanskiBroj = model.PostanskiBroj;
            item.BrojKreditneKartice = model.BrojKreditneKartice;
            item.Adresa = model.Adresa;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult ClientCreate()
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
    }
}
