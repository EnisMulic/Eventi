using System;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Data.EF;
using Eventi.Sdk;
using Eventi.Web.Areas.Client.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.Web.Areas.Client.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Client)]
    [Area("Client")]
    public class EventController : Controller
    {
        private readonly MojContext ctx;
        private readonly IEventiApi _eventiApi;

        public EventController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        [Obsolete]   
        public async Task<IActionResult> Index(int page=1)
        {
            var account = await HttpContext.GetLoggedInUser();
            if (account != null)
            {

                var clientResponse = await _eventiApi.GetClientAsync(new ClientSearchRequest()
                {
                    AccountID = account.ID
                });
                var Client = clientResponse.Content.Data.ToList()[0];

                var eventResponse = await _eventiApi.GetClientEvents(Client.ID);
                var events = eventResponse.Content;
                var model = new VisitedEventsVM()
                {
                    Events = eventResponse.Content
                        .Select
                        (
                            i => new VisitedEventsVM.Row
                            {
                                EventID = i.ID,
                                Category = i.EventCategory.ToString(),
                                Name = i.Name,
                                Image = i.Image,
                                Start = i.Start,
                                End = i.End
                            }
                        )
                        .ToList()
                };
                
                

                return View(model);
            }
            return Redirect("/Client/Client/Index");
        }
    }
}