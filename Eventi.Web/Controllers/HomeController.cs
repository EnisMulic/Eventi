using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eventi.Web.Models;
using Eventi.Data.Models;
using Eventi.Web.ViewModels;
using Eventi.Data.EF;
using Eventi.Web.Helper;
using Microsoft.EntityFrameworkCore;

namespace Eventi.Web.Controllers
{
    public class HomeController : Controller
    {  
      
        private readonly MojContext ctx;

        public HomeController(MojContext context)
        {
            ctx = context;
        }

        bool IsSoldOut(int EventId)
        {
           
            int MaxKarti = ctx.ProdajaTip.Where(i => i.EventId == EventId).Select(i => i.UkupnoKarataTip).Sum();
            int BrojProdanihKarti = ctx.ProdajaTip.Where(i => i.EventId == EventId).Select(i => i.BrojProdatihKarataTip).Sum();
            

            return BrojProdanihKarti == MaxKarti;
        }

        public IActionResult Index()
        {
             HttpContext.SetLogiraniUser(null);
            // kada se otvori stranica, modul je guest, i nijedan user jos nije logiran

            PretragaEventaVM model = new PretragaEventaVM();
            
            DateTime date = DateTime.Now;
            model.Eventi = ctx.Event.Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false)
                .Where(e => e.DatumOdrzavanja.CompareTo(date) == 1)
                .Select(e=>new PretragaEventaVM.Rows { 
                     EventId=e.Id,
                     Naziv=e.Naziv,
                     Kategorija=e.Kategorija.ToString(),
                     Slika=e.Slika
                }).ToList();

            foreach (var Event in model.Eventi)
                Event.SoldOut = IsSoldOut(Event.EventId);
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
