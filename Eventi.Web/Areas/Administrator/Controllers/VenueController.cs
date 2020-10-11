using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class VenueController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public VenueController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
        public IActionResult VenueList()
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult VenueUkloni(int Id)
        {
            var item = uow.ProstorOdrzavanjaRepository.Get(Id);

            if (item != null)
            {
                uow.ProstorOdrzavanjaRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult VenueInfo(int Id)
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult VenueUredi(int Id)
        {
            var model = uow.ProstorOdrzavanjaRepository.GetAll()
                .Select
                (
                    i => new ProstorOdrzavanjaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Adresa = i.Adresa,
                        TipProstoraOdrzavanja = i.TipProstoraOdrzavanja,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();
            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);
        }

        public IActionResult VenueSnimi(ProstorOdrzavanjaVM model)
        {
            var item = uow.ProstorOdrzavanjaRepository.Get(model.Id);


            item.Naziv = model.Naziv;
            item.Adresa = model.Adresa;
            item.TipProstoraOdrzavanja = model.TipProstoraOdrzavanja;
            item.GradId = model.GradId;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult VenueDodaj()
        {
            var model = new ProstorOdrzavanjaVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };
            return View(model);
        }

        public IActionResult ProstorDodajSnimi(ProstorOdrzavanjaVM model)
        {
            var item = new ProstorOdrzavanja
            {
                Naziv = model.Naziv,
                Adresa = model.Adresa,
                TipProstoraOdrzavanja = model.TipProstoraOdrzavanja,
                GradId = model.GradId
            };

            try
            {
                uow.ProstorOdrzavanjaRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
    }
}
