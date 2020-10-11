using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class HomeController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public HomeController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }

        public async Task<IActionResult> Index()
        {
            var user = await HttpContext.GetLoggedInUser();
            AdministratorVM model = new AdministratorVM();
            //if (user != null)
            //{
            //    model = uow.AdministratorRepository.GetAll()
            //        .Include(i => i.Osoba)
            //        .Where(i => i.Osoba.LogPodaciId == user.ID)
            //        .Select
            //        (
            //            i => new AdministratorVM
            //            {
            //                Id       = i.Id,
            //                Ime      = i.Osoba.Ime,
            //                Prezime  = i.Osoba.Prezime,
            //                Email    = i.Osoba.LogPodaci.Email,
            //                Username = i.Osoba.LogPodaci.Username,
            //                Password = i.Osoba.LogPodaci.Password,
            //                Telefon  = i.Osoba.Telefon,
            //                Grad     = i.Osoba.Grad.Naziv
            //            }
            //        )
            //        .SingleOrDefault();
                
            //}
            return View(model);
        }

        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }

        #region Drzava
        public IActionResult DrzavaList()
        {
            var model = uow.DrzavaRepository.GetAll()
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult DrzavaUkloni(int Id)
        {
            var item = uow.DrzavaRepository.Get(Id);

            if (item != null)
            {
                uow.DrzavaRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult DrzavaInfo(int Id)
        {
            var model = uow.DrzavaRepository.GetAll()
                .Select
                (
                    i => new DrzavaVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv
                    }
                )
                .Where(i => i.Id == Id)
                .FirstOrDefault();

            return View(model);
        }

        public IActionResult DrzavaUredi(int Id)
        {
            var Drzava = uow.DrzavaRepository.Get(Id);
            var model = new DrzavaVM
            {
                Id = Drzava.Id,
                Naziv = Drzava.Naziv
            };
            

            return View(model);
        }

        public IActionResult DrzavaSnimi(DrzavaVM model)
        {
            var item = uow.DrzavaRepository.Get(model.Id);
            item.Naziv = model.Naziv;

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult DrzavaDodaj() => View();

        public IActionResult DrzavaDodajSnimi(DrzavaVM model)
        {
            var item = new Drzava
            {
                Naziv = model.Naziv
            };

            try
            {
                uow.DrzavaRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }
            

            return Redirect("Index");
        }
        #endregion

        #region Grad
        public IActionResult GradList()
        {
            var model = uow.GradRepository.GetAll()
                .Select
                (
                    i => new GradVM
                    {
                        Id = i.Id,
                        Naziv = i.Naziv,
                        DrzavaNaziv = i.Drzava.Naziv
                    }
                )
                .ToList();

            return View(model);
        }

        public IActionResult GradUkloni(int Id)
        {
            var item = uow.GradRepository.Get(Id);

            if (item != null)
            {
                uow.GradRepository.Remove(Id);
            }

            return Redirect("Index");
        }

        public IActionResult GradInfo(int Id)
        {
            var Grad = uow.GradRepository.Get(Id);
            var model = new GradVM
            {
                Id = Grad.Id,
                Naziv = Grad.Naziv,
                DrzavaId = Grad.DrzavaId,
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradUredi(int Id)
        {
            var Grad = uow.GradRepository.Get(Id);
            var model = new GradVM
            {
                Id = Grad.Id,
                Naziv = Grad.Naziv,
                DrzavaId = Grad.DrzavaId,
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradSnimi(GradVM model)
        {
            var item = uow.GradRepository.Get(model.Id);
            item.Naziv = model.Naziv;
            item.DrzavaId = model.DrzavaId;
            

            ctx.SaveChanges();

            return Redirect("Index");
        }

        public IActionResult GradDodaj()
        {
            var model = new GradVM
            {
                Drzave = uow.DrzavaRepository.GetAll().Select(
                    i => new SelectListItem(i.Naziv, i.Id.ToString())).ToList()
            };

            return View(model);
        }

        public IActionResult GradDodajSnimi(GradVM model)
        {
            var item = new Grad
            {
                Naziv = model.Naziv,
                DrzavaId = model.DrzavaId
            };

            try
            {
                uow.GradRepository.Add(item);
            }
            catch //(Exception e)
            {
                //Console.WriteLine("{0} Exception caught.", e);
            }


            return Redirect("Index");
        }

       
        #endregion


       
        //public IActionResult EmailPostoji(string Email)
        //{
        //    var email = uow.LogPodaciRepository.GetAll()
        //        .SingleOrDefault(i => i.Email == Email);

        //    return Json(email != null);
        //}
    }
}