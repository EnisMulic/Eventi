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
    public class AdministratorController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public AdministratorController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }

        public async Task<IActionResult> AdminProfile(int ID)
        {
            var response = await _eventiApi.GetAdministratorAsync(new AdministratorSearchRequest() { AccountID = ID });
            var entity = response.Content.Data.ToList()[0];
            AdministratorVM model = new AdministratorVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Username = entity.Username,
                PhoneNumber = entity.PhoneNumber
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int ID)
        {
            var response = await _eventiApi.GetAdministratorAsync(ID);
            var entity = response.Content;
            AdministratorVM model = new AdministratorVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Username = entity.Username,
                PhoneNumber = entity.PhoneNumber
            };

            return View(model);
        }

        public async Task<IActionResult> Save(AdministratorVM model)
        {
            var request = new AdministratorUpdateRequest()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username
            };

            try
            {
                await _eventiApi.UpdateAdministratorAsync(model.ID, request);
                
            }
            catch
            {

            }

            return Redirect("AdminProfile?id=" + model.ID);
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            var account = await HttpContext.GetLoggedInUser();
            var response = await _eventiApi.GetAdministratorAsync(id);

            var admin = response.Content;
            var model = new AdministratorVM
            {
                ID = id,
                AccountID = admin.AccountID,
                Username = admin.Username
            };

            return View(model);
        }


        public IActionResult SavePassword(AdministratorVM model)
        {
            // To Do
            return Redirect("AdminProfil?id=" + model.ID);
        }
    }
}