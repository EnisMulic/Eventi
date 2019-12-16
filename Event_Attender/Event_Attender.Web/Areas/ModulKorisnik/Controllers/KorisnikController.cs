using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.ModulKorisnik.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Areas.ModulKorisnik.Controllers
{   
    [Autorizacija(korisnik:true,organizator:false,administrator:false,radnik:false)]
    [Area("ModulKorisnik")]
    public class KorisnikController : Controller
    {
        //private MojContext ctx;

        //public KorisnikController(MojContext context)
        //{
        //    ctx = context;
        //}
        public IActionResult Index()
        {   
            MojContext ctx = new MojContext();

            //// v1
            //int logPodaciId = HttpContext.GetLogiraniUser();
            //Korisnik k = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == logPodaciId).Include(k => k.Osoba).SingleOrDefault();
            //ViewData["k"] = k;

            // v2
            //LogPodaci l = HttpContext.GetLogiraniUser();
            //if (l != null)
            //{
            //    Korisnik k = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == l.Id).Include(k=>k.Osoba).SingleOrDefault();
            //    ViewData["k"] = k;
            //}
            //else
            //{
            //    ViewData["poruka"] = "vraceno null";
            //}

            
            PretragaEventaVM model = new PretragaEventaVM();
           
            DateTime date = DateTime.Now;
            model.eventi = ctx.Event.Where(e => e.IsOdobren == true).Where(e => e.IsOtkazan == false).Where(e => e.DatumOdrzavanja.CompareTo(date) == 1).ToList();
            //ctx.Dispose();
            return View(model);
        }
    }
}