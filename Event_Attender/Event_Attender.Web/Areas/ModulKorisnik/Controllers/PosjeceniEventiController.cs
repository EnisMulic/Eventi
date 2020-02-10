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
using ReflectionIT.Mvc.Paging;

namespace Event_Attender.Web.Areas.ModulKorisnik.Controllers
{
    [Area("ModulKorisnik")]
    public class PosjeceniEventiController : Controller
    {
        private readonly MojContext ctx;

        public PosjeceniEventiController(MojContext context)
        {
            ctx = context;
        }

        [Obsolete]   
        public async Task<IActionResult> Index(int page=1)
        {
            LogPodaci l = HttpContext.GetLogiraniUser();
            if (l != null)
            {
                Korisnik k = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == l.Id).Include(k => k.Osoba).SingleOrDefault();

                var posjeceni = ctx.Kupovina.Where(p => p.KorisnikId == k.Id)
                    .Select(p => new PrikazPosjeceniEventiVM
                    {
                        page=page,
                        KupovinaId = p.Id,
                        KorisnikId = k.Id,
                        EventId = p.EventId,
                        DatumOdrzavanja = p.Event.DatumOdrzavanja.ToShortDateString(),
                        Kategorija = p.Event.Kategorija.ToString(),
                        Naziv = p.Event.Naziv,
                        Slika = p.Event.Slika,
                        ProstorOdrzavanjaGrad = p.Event.ProstorOdrzavanja.Grad.Naziv,
                        VrijemeOdrzavanja = p.Event.VrijemeOdrzavanja,
                        UkupnoPlaceno = ctx.KupovinaTip.Where(t => t.KupovinaId == p.Id).Sum(t => t.Cijena)
                    }).AsNoTracking().OrderBy(p => p.KupovinaId);
                var model = await PagingList<PrikazPosjeceniEventiVM>.CreateAsync(posjeceni, 2, page);

                return View(model);
            }
            return Redirect("/ModulKorisnik/Korisnik/Index");
        }

        public IActionResult Recenzija(int id, int page)
        {
            Recenzija r = ctx.Recenzija.Where(re=>re.KupovinaId == id).SingleOrDefault();
            Kupovina k = ctx.Kupovina.Where(kp => kp.Id == id).Include(kp => kp.Event).SingleOrDefault();
            RecenzijaVM model = new RecenzijaVM();
            model.page = page;
            if (r == null)
            {
                // recenzija se tek dodaje
                model.RecenzijaId = 0;
                model.KupovinaId = id;
                model.NazivEventa = k.Event.Naziv;
            }
            else
            {
                // recenzija postoji
                model.RecenzijaId = r.Id;
                model.KupovinaId = r.KupovinaId;
                model.Komentar = r.Komentar;
                model.Ocjena = r.Ocjena;
                model.NazivEventa = k.Event.Naziv;
            }
              
            
            return PartialView(model);
        }

        public IActionResult SnimiRecenziju(RecenzijaVM model)
        {
            Recenzija r;
            if (model.RecenzijaId == 0)
            {
                r = new Recenzija();
                ctx.Recenzija.Add(r);
            }
            else
            {
               r = ctx.Recenzija.Where(re => re.Id==model.RecenzijaId).SingleOrDefault();
            }
            r.KupovinaId = model.KupovinaId;
            r.Komentar = model.Komentar;
            r.Ocjena = model.Ocjena;

            ctx.SaveChanges();
            return Redirect("/ModulKorisnik/PosjeceniEventi/Index?page="+model.page);
        }
    }
}