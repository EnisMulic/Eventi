using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventi.Web.Models;
using Eventi.Web.ViewModels;
using Eventi.Web.Helper;
using Eventi.Sdk;
using Eventi.Contracts.V1.Requests;

namespace Eventi.Web.Controllers
{
    public class HomeController : Controller
    {  
      
        private readonly IEventiApi _eventiApi;

        public HomeController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.SetLoggedInUser(null);
            // kada se otvori stranica, modul je guest, i nijedan user jos nije logiran

            PretragaEventaVM model = new PretragaEventaVM();
            
            DateTime date = DateTime.Now;
            var Events = await _eventiApi.GetEventAsync(
                new EventSearchRequest()
                {
                    IsApproved = true,
                    IsCanceled = false,
                    Start = DateTime.Now
                },
                new PaginationQuery()
            );
            model.Eventi = Events.Content.Data
                .Select(e => new PretragaEventaVM.Rows
                {
                    EventId = e.ID,
                    Naziv = e.Name,
                    Kategorija = e.EventCategory.ToString(),
                    Slika = e.Image
                }).ToList();


            return View(model);

        }

        public IActionResult Privacy() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
