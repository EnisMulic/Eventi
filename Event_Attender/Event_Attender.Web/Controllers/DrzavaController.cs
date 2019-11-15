using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventAttender.Data.EF;
using EventAttender.Data.Models;
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
                // nece ispisati drzave jer je baza prazna
            }
            return View();
        }
    }
}