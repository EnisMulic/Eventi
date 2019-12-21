using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        //private readonly MojContext ctx;

        //public HomeController(MojContext context)
        //{
        //    ctx = context;
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }

        //public IActionResult _AdminEventDisplay()
        //{
        //    return View();
        //}
    }
}