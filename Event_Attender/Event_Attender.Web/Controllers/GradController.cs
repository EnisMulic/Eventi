using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Controllers
{
    public class GradController : Controller
    {
        public IActionResult Index()
        {
            using(MojContext ctx=new MojContext())
            {
                List<Grad> gradovi = ctx.Grad.Include(g=>g.Drzava).ToList();
                ViewData["gradovi"] = gradovi;
            }
            return View();
        }
        public IActionResult Dodaj()
        {   
            ViewData["grad"] = new Grad();
            using(MojContext ctx=new MojContext())
            {
               ViewData["drzave"]= ctx.Drzava.ToList();
            }
            return View();
        }
        public IActionResult Snimi(int id, int drzavaId, string naziv)
        {
            Grad g;
            MojContext ctx = new MojContext();
            if (id == 0)
            {
                g= new Grad();
                ctx.Grad.Add(g);
            }
            else
            {
                g = ctx.Grad.Find(id);
            }
           
            g.Naziv = naziv;
            g.DrzavaId = drzavaId;
          
            ctx.SaveChanges();
            return Redirect("/Grad/Index");
        }
        public IActionResult Obrisi(int id)
        {
            using(MojContext ctx=new MojContext())
            {
                Grad g = ctx.Grad.Find(id);
                ctx.Grad.Remove(g);
                ctx.SaveChanges();
            }
            return Redirect("/Grad/Index");
        }
        public IActionResult Uredi(int id)
        {
            using (MojContext ctx = new MojContext())
            {
                ViewData["grad"] =  ctx.Grad.Find(id);
                ViewData["drzave"] = ctx.Drzava.ToList();
            }
            return View("Dodaj");
        }
    }
}