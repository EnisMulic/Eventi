using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class PerformerController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public PerformerController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
        public IActionResult PerformerList()
        {
            var model = uow.IzvodjacRepository.GetAll()
                .Select
                (
                    i => new IzvodjacVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        TipIzvodjaca = i.TipIzvodjaca
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult PerformerUkloni(int Id)
        {
            var item = uow.IzvodjacRepository.Get(Id);

            if (item != null)
            {
                uow.IzvodjacRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult PerformerInfo(int Id)
        {
            var i = uow.IzvodjacRepository.Get(Id);
            var model = new IzvodjacVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                TipIzvodjaca = i.TipIzvodjaca
            };

            return View(model);
        }

        public IActionResult PerformerUredi(int Id)
        {
            var i = uow.IzvodjacRepository.Get(Id);
            var model = new IzvodjacVM
            {
                Id = i.Id,
                Naziv = i.Naziv,
                TipIzvodjaca = i.TipIzvodjaca
            };


            return View(model);
        }

        public IActionResult IzvodjacSnimi(IzvodjacVM model)
        {
            var item = uow.IzvodjacRepository.Get(model.Id);


            item.Naziv = model.Naziv;
            item.TipIzvodjaca = model.TipIzvodjaca;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult PerformerDodaj()
        {
            var model = new IzvodjacVM();

            return View(model);
        }

        public IActionResult IzvodjacDodajSnimi(IzvodjacVM model)
        {
            var item = new Izvodjac
            {
                Naziv = model.Naziv,
                TipIzvodjaca = model.TipIzvodjaca
            };

            try
            {
                uow.IzvodjacRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }
    }
}
