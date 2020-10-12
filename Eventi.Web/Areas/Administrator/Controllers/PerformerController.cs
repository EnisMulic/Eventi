using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class PerformerController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public PerformerController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        public async Task<IActionResult> PerformerList()
        {
            var response = await _eventiApi.GetPerformerAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new PerformerVM
                    {
                        ID = i.ID,
                        Name = i.Name,
                        PerformerCategory = i.PerformerCategory
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> PerformerRemove(int ID)
        {
            await _eventiApi.DeletePerformerAsync(ID);

            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> PerformerDetails(int ID)
        {
            var response = await _eventiApi.GetPerformerAsync(ID);
            var entity = response.Content;
            var model = new PerformerVM
            {
                ID = entity.ID,
                Name = entity.Name,
                PerformerCategory = entity.PerformerCategory
            };

            return View(model);
        }

        public async Task<IActionResult> PerformerEdit(int ID)
        {
            var response = await _eventiApi.GetPerformerAsync(ID);
            var entity = response.Content;
            var model = new PerformerVM
            {
                ID = entity.ID,
                Name = entity.Name,
                PerformerCategory = entity.PerformerCategory
            };


            return View(model);
        }

        public async Task<IActionResult> PerformerSave(PerformerVM model)
        {
            var request = new PerformerUpsertRequest()
            {
                Name = model.Name,
                PerformerCategory = model.PerformerCategory
            };


            if(model.ID == 0)
            {
                await _eventiApi.CreatePerformerAsync(request);
            }
            else
            {
                await _eventiApi.UpdatePerformerAsync(model.ID, request);
            }

            return Redirect("/Administrator/Home/Index");
        }

        public IActionResult PerformerCreate()
        {
            var model = new PerformerVM();

            return View(model);
        }
    }
}
