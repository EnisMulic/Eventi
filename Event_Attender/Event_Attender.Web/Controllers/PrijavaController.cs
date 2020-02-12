using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Event_Attender.Web.ViewModels;

namespace Event_Attender.Web.Controllers
{
    public class PrijavaController : Controller
    {
        private MojContext ctx;

        public PrijavaController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }

        public IActionResult LogIn( LoginVM input)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", input);
            }
            Korisnik k =null; 
            List<Korisnik> korisnici = ctx.Korisnik.Include(o=>o.Osoba).Include(o=>o.Osoba.LogPodaci).ToList();
            foreach (Korisnik kor in korisnici)
            {
                if (kor.Osoba.LogPodaci.Username.Equals(input.username) && kor.Osoba.LogPodaci.Password.Equals(input.password))
                {
                    k = kor;
                }
            }
            if (k != null)
            {
                HttpContext.SetLogiraniUser(k.Osoba.LogPodaci);

                return Redirect("/ModulKorisnik/Korisnik/Index");
            }

            
            Administrator a = null;
            List<Administrator> administratori = ctx.Administrator.Include(o => o.Osoba).Include(o => o.Osoba.LogPodaci).ToList();
            foreach (Administrator adm in administratori)
            {
                if (adm.Osoba.LogPodaci.Username.Equals(input.username) && adm.Osoba.LogPodaci.Password.Equals(input.password))
                {
                    a=adm;
                }
            }
            if (a != null)
            {
                HttpContext.SetLogiraniUser(a.Osoba.LogPodaci);
                return Redirect("/Administrator/Home/Index");
            }

            Radnik r = null;
            List<Radnik> radnici = ctx.Radnik.Include(o => o.Osoba).Include(o => o.Osoba.LogPodaci).ToList();
            foreach (Radnik rad in radnici)
            {
                if (rad.Osoba.LogPodaci.Username.Equals(input.username) && rad.Osoba.LogPodaci.Password.Equals(input.password))
                {
                    r = rad;
                }
            }
            if (r != null)
            {
                HttpContext.SetLogiraniUser(r.Osoba.LogPodaci);
                return Redirect("/ModulRadnik/Radnik/Index");
                
            }

            Organizator o = null;
            List<Organizator> organizatori = ctx.Organizator.Include(s=>s.LogPodaci).ToList();
            foreach (Organizator org in organizatori)
            {
                if (org.LogPodaci.Username.Equals(input.username) && org.LogPodaci.Password.Equals(input.password))
                {
                    o = org;
                }
            }
            if (o != null)
            {
                HttpContext.SetLogiraniUser(o.LogPodaci);
                return Redirect("/OrganizatorModul/OrganizatorHome/Index");
            }
         
            TempData["error_poruka"] = "Niste unijeli ispravne podatke za prijavu";
            return RedirectToAction("Index");  // opet vraca na prijavu
          
        }

        
        public IActionResult LogOut()
        {
            HttpContext.RemoveCookie(); 
            return RedirectToAction("Index", "Home","");
        }
    }
}