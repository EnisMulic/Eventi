using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Event_Attender.Web.Models;
using EventAttender.Data.Models;
using Event_Attender.Web.ViewModels;
using EventAttender.Data.EF;

namespace Event_Attender.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PretragaEventaVM model = new PretragaEventaVM();
            MojContext ctx = new MojContext();
            model.eventi = ctx.Event.Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).ToList();
            ctx.Dispose();
            return View(model);

        }

        // U slucaju da pretraga po nazivu i pretraga po lakaciji ne idu kao 2 odvojene
        // pretrage, 2 buttona, moze se vrsiti pretraga i po nazivu i po lokaciji zajedno
        // u ovom slucaju se ne otvaraju novi view, vec ponovo HomePage tj view Index

        //public IActionResult Index(string filter)  // v2 - pretrazuje i po nazivu i po lokaciji
        //{
        //    PretragaEventaVM model = new PretragaEventaVM();
        //    MojContext ctx = new MojContext();


        //    if (filter == null)
        //    {
        //        model.eventi = ctx.Event.Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).ToList();
        //    }
        //    else
        //    {
        //        model.eventi = ctx.Event.Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).
        //               Where(e => e.Naziv.ToLower().StartsWith(filter.ToLower())
        //                 || e.Naziv.ToLower().Contains(filter.ToLower()) ||
        //                  e.ProstorOdrzavanja.Naziv.ToLower().StartsWith(filter.ToLower()) || e.ProstorOdrzavanja.Naziv.ToLower().Contains(filter.ToLower())
        //                 || e.ProstorOdrzavanja.Grad.Naziv.ToLower().StartsWith(filter.ToLower()) || e.ProstorOdrzavanja.Grad.Naziv.ToLower().Contains(filter.ToLower())).ToList();
        //    }

        //    return View("Index", model);
        //}
        public IActionResult Prijava()
        {
            return View("Index");
        }

        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
