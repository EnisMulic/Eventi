using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Data.Models;
using Eventi.Sdk;
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
        private readonly IEventiApi _eventiApi;
        public OrganizerController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> OrganizerList()
        {
            var response = await _eventiApi.GetOrganizerAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new OrganizerVM
                    {
                        ID = i.ID,
                        AccountID = i.AccountID,
                        Name = i.Name,
                        PhoneNumber = i.PhoneNumber,
                        CityID = i.CityID,
                        Username = i.Username,
                        Email = i.Email
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> OrganizerRemove(int ID)
        {
            await _eventiApi.DeleteOrganizerAsync(ID);
            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> OrganizerDetails(int ID)
        {
            var response = await _eventiApi.GetOrganizerAsync(ID);
            var entity = response.Content;
            var model = new OrganizerVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                Name = entity.Name,
                Username = entity.Username,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                CityID = entity.CityID
            };
            return View(model);
        }

        public async Task<IActionResult> OrganizerEdit(int ID)
        {
            var response = await _eventiApi.GetOrganizerAsync(ID);
            var entity = response.Content;
            var model = new OrganizerVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                Name = entity.Name,
                Username = entity.Username,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                CityID = entity.CityID
            };
            return View(model);
        }

        public async Task<IActionResult> OrganizerSave(OrganizerVM model)
        {
            if(model.ID == 0)
            {
                var insertRequest = new OrganizerInsertRequest()
                {
                    Name = model.Name,
                    CityID = model.CityID,
                    PhoneNumber = model.PhoneNumber
                };

                await _eventiApi.CreateOrganizerAsync(insertRequest);
            }
            else
            {
                var updateRequest = new OrganizerUpdateRequest()
                {
                    Name = model.Name,
                    CityID = model.CityID,
                    PhoneNumber = model.PhoneNumber
                };

                await _eventiApi.UpdateOrganizerAsync(model.ID, updateRequest);
            }

            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> OrganizerCreate()
        {
            var response = await _eventiApi.GetCityAsync();
            var Cities = response.Content.Data;
            var model = new OrganizerVM
            {
                Cities = Cities.Select(i => new SelectListItem(i.Name, i.ID.ToString())).ToList()
            };

            return View(model);
        }
    }
}
