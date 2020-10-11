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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class OrganizerController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public OrganizerController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
        public IActionResult OrganizerList()
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email

                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult OrganizerUkloni(int Id)
        {
            var item = uow.OrganizatorRepository.Get(Id);

            if (item != null)
            {

                uow.OrganizatorRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult OrganizerInfo(int Id)
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email,
                        Password = i.LogPodaci.Password
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult OrganizerUredi(int Id)
        {
            var model = uow.OrganizatorRepository.GetAll()
                .Select
                (
                    i => new OrganizatorVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        Telefon = i.Telefon,
                        GradId = i.Grad.Id,
                        GradNaziv = i.Grad.Naziv,
                        Username = i.LogPodaci.Username,
                        Email = i.LogPodaci.Email,
                        Password = i.LogPodaci.Password,
                        LogPodaciId = i.LogPodaci.Id
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            model.Gradovi = uow.GradRepository.GetAll().Select(
                i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList();

            return View(model);


        }

        public IActionResult OrganizerSnimi(OrganizatorVM model)
        {
            var item = uow.OrganizatorRepository.GetAll()
                .Include(i => i.LogPodaci)
                .Where(i => i.Id == model.Id)
                .FirstOrDefault();

            if (item.LogPodaciId == null)
            {
                item.LogPodaci = new LogPodaci();
            }

            item.Naziv = model.Naziv;
            item.LogPodaci.Username = model.Username;
            item.LogPodaci.Email = model.Email;
            item.LogPodaci.Password = model.Password;
            item.GradId = model.GradId;
            item.Telefon = model.Telefon;


            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult OrganizerDodaj()
        {
            var model = new OrganizatorVM
            {
                Gradovi = uow.GradRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }



        public IActionResult OrganizerDodajSnimi(OrganizatorVM model)
        {

            var log = new LogPodaci
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            uow.LogPodaciRepository.Add(log);


            var item = new Organizator
            {
                Naziv = model.Naziv,
                Telefon = model.Telefon,
                GradId = model.GradId,
                LogPodaci = log
            };

            try
            {
                uow.OrganizatorRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }




            return Redirect("Index");
        }
    }
}
