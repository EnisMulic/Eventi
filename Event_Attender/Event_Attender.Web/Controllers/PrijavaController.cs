using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Controllers
{
    public class PrijavaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn(string username, string password)
        {
            MojContext ctx = new MojContext();

            Korisnik k= ctx.Korisnik.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == username && o.Osoba.LogPodaci.Password == password).SingleOrDefault();
            Administrator a= ctx.Administrator.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == username && o.Osoba.LogPodaci.Password == password).SingleOrDefault();
            Radnik r = ctx.Radnik.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == username && o.Osoba.LogPodaci.Password == password).SingleOrDefault();
            Organizator o = ctx.Organizator.Include(o => o.LogPodaci).Where(o => o.LogPodaci.Username == username && o.LogPodaci.Password == password).SingleOrDefault();
            if (k != null)
            {
                
                HttpContext.SetLogiraniUser(k.Osoba.LogPodaci);

                return Redirect("/ModulKorisnik/Korisnik/Index");
            }
            if(a != null)
            {
                return Redirect("/Administrator/Home/Index");
            }
            if (r != null)
            {                    // radnik/Index
                return Redirect("/Home/Index");
            }
            if (o != null)
            {
                return Redirect("/OrganizatorModul/OrganizatorHome/Index");
            }
            else
            {
                //korisnik ne postoji
                TempData["error_poruka"] = "Niste unijeli ispravne podatke za prijavu";
                return RedirectToAction("Index");  // opet vraca na prijavu
            }
        }

        
        public IActionResult LogOut()
        {
            HttpContext.RemoveCookie();  //isto se desava
            return RedirectToAction("Index", "Home","");
        }
    }
}