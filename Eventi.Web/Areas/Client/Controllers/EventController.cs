using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Web.Areas.Client.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace Eventi.Web.Areas.Client.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Client)]
    [Area("Client")]
    public class EventController : Controller
    {
        private readonly MojContext ctx;

        public EventController(MojContext context)
        {
            ctx = context;
        }

        [Obsolete]   
        public async Task<IActionResult> Index(int page=1)
        {
            var l = await HttpContext.GetLoggedInUser();
            if (l != null)
            {
                Korisnik k = ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == l.ID).Include(k => k.Osoba).SingleOrDefault();

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
    }
}