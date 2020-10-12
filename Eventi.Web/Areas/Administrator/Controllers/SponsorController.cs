using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class SponsorController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public SponsorController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        public async Task<IActionResult> SponsorList()
        {
            var response = await _eventiApi.GetSponsorAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new SponsorVM
                    {
                        ID = i.ID,
                        Name = i.Name,
                        PhoneNumber = i.PhoneNumber,
                        Email = i.Email
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> SponsorRemove(int ID)
        {
            await _eventiApi.DeleteSponsorAsync(ID);
            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> SponsorDetails(int ID)
        {
            var response = await _eventiApi.GetSponsorAsync(ID);
            var entity = response.Content;
            var model = new SponsorVM
            {
                ID = entity.ID,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };

            return View(model);
        }

        public async Task<IActionResult> SponsorEdit(int ID)
        {
            var response = await _eventiApi.GetSponsorAsync(ID);
            var entity = response.Content;
            var model = new SponsorVM
            {
                ID = entity.ID,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };

            return View(model);
        }

        public async Task<IActionResult> SponsorSave(SponsorVM model)
        {
            var request = new SponsorUpsertRequest()
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };


            if (model.ID == 0)
            {
                await _eventiApi.CreateSponsorAsync(request);
            }
            else
            {
                await _eventiApi.UpdateSponsorAsync(model.ID, request);
            }

            return Redirect("/Administrator/Home/Index");
        }

        public IActionResult SponsorCreate()
        {
            SponsorVM model = new SponsorVM();
            return View(model);
        }
    }
}
