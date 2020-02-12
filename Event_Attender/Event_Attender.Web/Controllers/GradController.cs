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
        private readonly MojContext ctx;

        public GradController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
           
                List<Grad> gradovi = ctx.Grad.Include(g=>g.Drzava).ToList();
                ViewData["gradovi"] = gradovi;
           
            return View();
        }
        public IActionResult Dodaj()
        {   
            ViewData["grad"] = new Grad();
           
               ViewData["drzave"]= ctx.Drzava.ToList();
           
            return View();
        }
        public IActionResult Snimi(int id, int drzavaId, string naziv)
        {
            Grad g;
          
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
            
                Grad g = ctx.Grad.Find(id);
                ctx.Grad.Remove(g);
                ctx.SaveChanges();
           
            return Redirect("/Grad/Index");
        }
        public IActionResult Uredi(int id)
        {
          
                ViewData["grad"] =  ctx.Grad.Find(id);
                ViewData["drzave"] = ctx.Drzava.ToList();
            
            return View("Dodaj");
        }
    }
}