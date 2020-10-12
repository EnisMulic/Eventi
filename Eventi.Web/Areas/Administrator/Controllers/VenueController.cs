using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class VenueController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public VenueController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> VenueList()
        {
            var response = await _eventiApi.GetVenueAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new VenueVM
                    {
                        ID = i.ID,
                        Name = i.Name,
                        Address = i.Address,
                        VenueCategory = i.VenueCategory,
                        CityID = i.CityID
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> VenueRemove(int ID)
        {
            await _eventiApi.DeleteVenueAsync(ID);
            return Redirect("Index");
        }

        public async Task<IActionResult> VenueDetails(int ID)
        {
            var response = await _eventiApi.GetVenueAsync(ID);
            var entity = response.Content;
            var model = new VenueVM
            {
                ID = entity.ID,
                Name = entity.Name,
                Address = entity.Address,
                VenueCategory = entity.VenueCategory,
                CityID = entity.CityID
            };

            return View(model);
        }

        public async Task<IActionResult> VenueEdit(int ID)
        {
            var response = await _eventiApi.GetVenueAsync(ID);
            var entity = response.Content;
            var model = new VenueVM
            {
                ID = entity.ID,
                Name = entity.Name,
                Address = entity.Address,
                VenueCategory = entity.VenueCategory,
                CityID = entity.CityID
            };

            return View(model);
        }

        public async Task<IActionResult> VenueSave(VenueVM model)
        {
            var request = new VenueUpsertRequest()
            {
                Name = model.Name,
                Address = model.Address,
                VenueCategory = model.VenueCategory,
                CityID = model.CityID
            };

            if(model.ID == 0)
            {
                await _eventiApi.CreateVenueAsync(request);
            }
            else
            {
                await _eventiApi.UpdateVenueAsync(model.ID, request);
            }

            return Redirect("Index");
        }

        public async Task<IActionResult> VenueCreate()
        {
            var response = await _eventiApi.GetCityAsync();
            var Cities = response.Content.Data;
            var model = new VenueVM
            {
                Cities = Cities.Select(i => new SelectListItem(i.Name, i.ID.ToString())).ToList()
            };
            return View(model);
        }
    }
}
