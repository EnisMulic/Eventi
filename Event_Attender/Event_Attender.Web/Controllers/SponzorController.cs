using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Web.ViewModels;
using EventAttender.Data.EF;
using EventAttender.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Controllers
{
    public class SponzorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SponzorGetAll()
        {
            using (MojContext ctx = new MojContext())
            {
                List<SponzorVM> sponzori = ctx.Sponzor.Select(s => new SponzorVM
                {
                    Naziv = s.Naziv,
                    Email = s.Email,
                    Telefon = s.Telefon
                }).ToList();

                ViewData["sponzorKljuc"] = sponzori;
                return View();
            }

        }

        public IActionResult SponzorAddForm()
        {
            return View();
        }

        public IActionResult AddSponzor(string _naziv, string _telefon, string _email)
        {
            Sponzor s = new Sponzor
            {
                Naziv = _naziv,
                Telefon = _telefon,
                Email = _email
            };

            using (MojContext ctx = new MojContext())
            {
                ctx.Sponzor.Add(s);
                ctx.SaveChanges();
            }
            return Redirect("SponzorGetAll");
        }
    }
}