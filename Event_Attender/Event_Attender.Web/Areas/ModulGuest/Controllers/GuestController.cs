using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Web.Areas.ModulGuest.Models;
using Event_Attender.Data.EF;
using Microsoft.AspNetCore.Mvc;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Event_Attender.Web.Areas.ModulGuest.Controllers
{   
    [Area("ModulGuest")]
    public class GuestController : Controller
    {
        private readonly MojContext ctx;

        public GuestController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult PretraziPoNazivu(string filter)  // v1- odvojena pretraga po lokaciji
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
        public IActionResult PretraziPoLokaciji(string lokacija)  //v1 - odvojena pretraga po nazivu
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
        public IActionResult RegistracijaForma()
        {
          
            RegistracijaVM model = new RegistracijaVM();
            model.Drzave = ctx.Drzava.Select(d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList();

            return View(model);
        }

        public bool VerifyUserName(string username)
        {
            // trebali bi se povuci svi username iz baze, pa provjeravati da li vec postoji isti
        
            List<LogPodaci> logPodaci = ctx.LogPodaci.ToList();
            if (logPodaci == null)
            {
                return true;// prazna lista
            }
            else
            {
                foreach (LogPodaci l in logPodaci)
                {
                    if (l.Username.Equals(username))
                        return false;
                }
            }
         
            return true;
        }
        public IActionResult RegistracijaSnimi(RegistracijaVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Drzave = ctx.Drzava.Select(d => new SelectListItem(d.Naziv, d.Id.ToString())).ToList();
                return View("RegistracijaForma", model);
            }
          
            Korisnik k = new Korisnik();
            k.Osoba = new Osoba
            {
                Ime = model.Ime,
                Prezime = model.Prezime,
                Telefon = model.Telefon
            };

            k.Osoba.LogPodaci = new LogPodaci
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email
            };

            List<Grad> gradovi = ctx.Grad.ToList();

            model.Grad = model.Grad.Replace("ć", "c");
            model.Grad = model.Grad.Replace("č", "c");
            model.Grad = model.Grad.Replace("š", "s");
            model.Grad = model.Grad.Replace("đ", "d");
            model.Grad = model.Grad.Replace("ž", "z");
            model.Grad = model.Grad.Replace("Ć", "C");
            model.Grad = model.Grad.Replace("Č", "C");
            model.Grad = model.Grad.Replace("Š", "S");
            model.Grad = model.Grad.Replace("Đ", "D");
            model.Grad = model.Grad.Replace("Ž", "Z");
            foreach (Grad g in gradovi)
            {
                if (model.Grad.ToLower().Equals(g.Naziv.ToLower()))
                {
                    k.Osoba.GradId = g.Id;
                    break;
                }
            }
            if (k.Osoba.GradId == 0)  // znaci da nema u bazi, 
            {
                k.Osoba.Grad = new Grad { Naziv = model.Grad, DrzavaId = model.DrzavaId };
            }
            k.Adresa = model.Adresa;
            k.PostanskiBroj = model.PostanskiBroj;
            k.BrojKreditneKartice = model.BrojKreditneKartice;

            ctx.Korisnik.Add(k);
            ctx.SaveChanges();
           
            return Redirect("/Prijava/Index");
        }
    }
}
