using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Microsoft.AspNetCore.Mvc;

namespace Event_Attender.Web.Areas.ModulRadnik.Controllers
{
    [Area("ModulRadnik")]
    public class RadnikController : Controller
    {

        private readonly MojContext ctx;

        public RadnikController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}