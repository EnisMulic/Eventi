using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Web.Areas.ModulRadnik.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Event_Attender.Web.Areas.ModulRadnik.Controllers
{
    [Area("ModulRadnik")]
    public class RadnikController : Controller
    {

        private readonly MojContext ctx;

        public RadnikController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public string GetProizvodi()
        {
            LogPodaci l = HttpContext.GetLogiraniUser();

            Radnik radnik = ctx.Radnik.Where(ra => ra.Osoba.LogPodaciId == l.Id).SingleOrDefault();
            //if (r == null)
            //{
            //    return Redirect("/Prijava/LogIn");
            //}
            PrikazEvenataVM model = new PrikazEvenataVM();
            model.eventi = ctx.RadnikEvent.Where(r => r.Id == radnik.Id)
                .Select(r => new PrikazEvenataVM.Rows
                {
                    EventId = r.EventId,
                    NazivEventa = r.Event.Naziv,
                    DatumOdrzavanja = r.Event.DatumOdrzavanja.ToShortDateString(),
                    Grad = r.Event.ProstorOdrzavanja.Grad.Naziv,
                    ProstorOdrzavanjaIAdresa = r.Event.ProstorOdrzavanja.Naziv + " " + r.Event.ProstorOdrzavanja.Adresa,
                    RadnikEventId = r.Id,
                    RadnikId = r.RadnikId,
                    Vrijeme = r.Event.VrijemeOdrzavanja,
                   // UkupnoZaradaOdEventa = ctx.KupovinaTip.Where(k => k.Kupovina.EventId == r.EventId).Sum(k => k.Cijena)
                }).ToList();

            string eventi = JsonConvert.SerializeObject(model.eventi);
            return eventi;

        }

        //public IActionResult Detalji(int id)
        //{
        //    Event ev = ctx.Event.Where(e => e.Id == id).SingleOrDefault();

        //    return View();
        //}
    }
}