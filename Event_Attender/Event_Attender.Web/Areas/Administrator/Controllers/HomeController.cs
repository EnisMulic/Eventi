using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _AdminSidebar()
        {
            return PartialView();
        }

        public IActionResult _AdminEventDisplay()
        {
            return View();
        }
    }
}