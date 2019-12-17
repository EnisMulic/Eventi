﻿using System;
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
        public IActionResult Index()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }

        public IActionResult LogIn( LoginVM input)
        {
            MojContext ctx = new MojContext();

            Korisnik k= ctx.Korisnik.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == input.username && o.Osoba.LogPodaci.Password == input.password).SingleOrDefault();
            if (k != null)
            {
                HttpContext.SetLogiraniUser(k.Osoba.LogPodaci);

                return Redirect("/ModulKorisnik/Korisnik/Index");
            }

            Administrator a= ctx.Administrator.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == input.username && o.Osoba.LogPodaci.Password == input.password).SingleOrDefault();
            if (a != null)
            {
                HttpContext.SetLogiraniUser(a.Osoba.LogPodaci);
                return Redirect("/Administrator/Home/Index");
            }

            Radnik r = ctx.Radnik.Include(k => k.Osoba).Include(o => o.Osoba.LogPodaci)
                .Where(o => o.Osoba.LogPodaci.Username == input.username && o.Osoba.LogPodaci.Password == input.password).SingleOrDefault();
            if (r != null)
            {
                HttpContext.SetLogiraniUser(r.Osoba.LogPodaci);
                // radnik/Index 
                return RedirectToAction("Inex", "Home", "");
            }

            Organizator o = ctx.Organizator.Include(o => o.LogPodaci).Where(o => o.LogPodaci.Username == input.username && o.LogPodaci.Password == input.password).SingleOrDefault();
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
            HttpContext.RemoveCookie();  //?
            return RedirectToAction("Index", "Home","");
        }
    }
}