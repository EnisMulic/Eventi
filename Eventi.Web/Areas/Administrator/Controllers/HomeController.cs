using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Data.EF;
using Eventi.Data.Repository;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        //public IActionResult EmailPostoji(string Email)
        //{
        //    var email = uow.LogPodaciRepository.GetAll()
        //        .SingleOrDefault(i => i.Email == Email);

        //    return Json(email != null);
        //}
    }
}