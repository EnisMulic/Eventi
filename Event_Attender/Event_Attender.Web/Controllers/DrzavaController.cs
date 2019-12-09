using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Controllers
{
    public class DrzavaController : Controller
    {
        public IActionResult Index()
        {
            using(MojContext ctx=new MojContext())
            {
                List<Drzava> drzave = ctx.Drzava.ToList();
                ViewData["drzave"] = drzave;
            }
            return View();
        }
        public IActionResult DodajForma(int poziv, int gradId)
        {
            ViewData["drzava"] = new Drzava();
            ViewData["poziv"] = poziv;
            ViewData["gradId"] = gradId;
            return View();
        }
        public IActionResult Uredi(int id)
        {
            using(MojContext ctx= new MojContext())
            {
                ViewData["drzava"]= ctx.Drzava.Find(id);
                ViewData["poziv"] = 0;
                ViewData["gradId"] = 0;
            }
            return View("DodajForma");
        }
        public IActionResult Snimi(int id,string naziv, int poziv, int gradId)
        {
            MojContext ctx = new MojContext();
            Drzava d;
            if (id == 0)
            {
                // dodaje se nova drzava
                d = new Drzava();
                ctx.Drzava.Add(d);
            }
            else
            {
                d = ctx.Drzava.Find(id);
            }
            d.Naziv = naziv;
           
            ctx.SaveChanges();
            if (poziv == 2)
            {   if (gradId != 0)
                    return Redirect("/Grad/Uredi?id=" + gradId);
                return Redirect("/Grad/Dodaj");
            }
            return Redirect("/Drzava/Index");
           
        }
        public IActionResult Obrisi(int id)
        {   using(MojContext ctx=new MojContext())
            {
                Drzava d = ctx.Drzava.Find(id);
                ctx.Drzava.Remove(d);
                ctx.SaveChanges();
            }
         
            return Redirect("/Drzava/Index");
        }
    }
}