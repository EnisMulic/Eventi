using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class SponsorController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public SponsorController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }

        public IActionResult SponsorList()
        {
            var model = uow.SponzorRepository.GetAll()
                .Select
                (
                    i => new SponzorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        Email = i.Email
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult SponsorUkloni(int Id)
        {
            var item = uow.SponzorRepository.Get(Id);

            if (item != null)
            {
                uow.SponzorRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult SponsorInfo(int Id)
        {
            var i = uow.SponzorRepository.Get(Id);
            var model = new SponzorVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                Telefon = i.Telefon,
                Email = i.Email
            };

            return View(model);
        }

        public IActionResult SponsorUredi(int Id)
        {
            var i = uow.SponzorRepository.Get(Id);
            var model = new SponzorVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                Telefon = i.Telefon,
                Email = i.Email
            };

            return View(model);
        }

        public IActionResult SponsorSnimi(SponzorVM model)
        {
            var item = uow.SponzorRepository.Get(model.Id);
            item.Naziv = model.Naziv;
            item.Telefon = model.Telefon;
            item.Email = model.Email;

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult SponsorDodaj() => View();

        public IActionResult SponzorDodajSnimi(SponzorVM model)
        {
            var item = new Sponzor
            {
                Naziv = model.Naziv,
                Telefon = model.Telefon,
                Email = model.Email
            };

            try
            {
                uow.SponzorRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
    }
}
