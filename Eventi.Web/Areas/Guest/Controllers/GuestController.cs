using System;
using System.Collections.Generic;
using System.Linq;
using Eventi.Web.Areas.Guest.Models;
using Eventi.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eventi.Sdk;
using System.Threading.Tasks;
using AspNetCore;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Web.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class GuestController : Controller
    {
        private readonly MojContext ctx;
        private readonly IEventiApi _eventiApi;
        private readonly IAuthApi _authApi;

        public GuestController(MojContext context, IEventiApi eventiApi, IAuthApi authApi)
        {
            _eventiApi = eventiApi;
            _authApi = authApi;
            ctx = context;
        }
        public IActionResult PretraziPoNazivu(string filter)  // v1- odvojena pretraga po nazivu
        {
            PretragaEventaVM model = new PretragaEventaVM();
           
           
            DateTime date = DateTime.Now;
            //Where(e => e.DatumOdrzavanja.CompareTo(date)==1) // gdje je datum veci od danasnjeg
            if (filter != null)
            {
                
                model.Eventi = ctx.Event.Include(e => e.ProstorOdrzavanja).Include(e => e.ProstorOdrzavanja.Grad).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1).Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).
                    Where(e => e.Naziv.ToLower().Equals(filter.ToLower()) || e.Naziv.ToLower().StartsWith(filter.ToLower())
                     || e.Naziv.ToLower().Contains(filter.ToLower()))
                    .Select(e => new PretragaEventaVM.Rows {
                        EventId = e.Id,
                        Naziv = e.Naziv,
                        Kategorija = e.Kategorija.ToString(),
                        ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                        ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                        DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                        VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                        Slika = e.Slika
                    }).ToList();
            }
       
            return View(model);  
        }
        public IActionResult PretraziPoLokaciji(string lokacija)  //v1 - odvojena pretraga po lokaciji
        {
            PretragaEventaVM model = new PretragaEventaVM();
         
            DateTime date = DateTime.Now;
            if (lokacija != null)
            {
                model.Eventi = ctx.Event.Include(e=>e.ProstorOdrzavanja).Include(e=>e.ProstorOdrzavanja.Grad).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1).Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).
                   Where(e => e.ProstorOdrzavanja.Grad.Drzava.Naziv.ToLower().StartsWith(lokacija.ToLower()) || e.ProstorOdrzavanja.Naziv.ToLower().StartsWith(lokacija.ToLower()) || e.ProstorOdrzavanja.Naziv.ToLower().Contains(lokacija.ToLower())
                      || e.ProstorOdrzavanja.Grad.Naziv.ToLower().StartsWith(lokacija.ToLower()) || e.ProstorOdrzavanja.Grad.Naziv.ToLower().Contains(lokacija.ToLower()))
                   .Select(e=>new PretragaEventaVM.Rows {
                       EventId = e.Id,
                       Naziv = e.Naziv,
                       Kategorija = e.Kategorija.ToString(),
                       ProstorOdrzavanjaNaziv = e.ProstorOdrzavanja.Naziv,
                       ProstorOdrzavanjaGrad = e.ProstorOdrzavanja.Grad.Naziv,
                       DatumOdrzavanja = e.DatumOdrzavanja.Day.ToString() + "." + e.DatumOdrzavanja.Month.ToString() + "." + e.DatumOdrzavanja.Year.ToString(),
                       VrijemeOdrzavanja = e.VrijemeOdrzavanja,
                       Slika = e.Slika
                   }).ToList();
            }
         
            return View(model);
        }
        public async Task<IActionResult> Registration()
        {
            var countriesResponse = await _eventiApi.GetCountryAsync();
            var citiesResponse = await _eventiApi.GetCityAsync();

            var countries = countriesResponse.Content.Data
                .Select
                (
                    i => new SelectListItem()
                    {
                        Value = i.ID.ToString(),
                        Text = i.Name
                    }
                )
                .ToList();

            var cities = citiesResponse.Content.Data
                .Select
                (
                    i => new SelectListItem()
                    {
                        Value = i.ID.ToString(),
                        Text = i.Name
                    }
                )
                .ToList();

            RegistrationVM model = new RegistrationVM()
            {
                Cities = cities,
                Countries = countries
            };

            return View(model);
        }

        public async Task<IActionResult> Registrater(RegistrationVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = ctx.Drzava.Select(d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList();
                return View("RegistracijaForma", model);
            }

            var registration = new ClientRegistrationRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.LastName,
                CreditCardNumber = model.CreditCardNumber,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Username = model.Username
            };


            await _authApi.ClientRegisterAsync(registration);
            return Redirect("/Login/Index");
        }
    }
}
