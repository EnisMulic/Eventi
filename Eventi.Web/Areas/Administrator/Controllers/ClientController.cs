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
    public class ClientController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public ClientController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> ClientList()
        {
            var response = await _eventiApi.GetClientAsync();
            var model = response.Content.Data
                .Select
                (
                    i => new ClientVM
                    {
                        ID = i.ID,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Username = i.Username,
                        Email = i.Email,
                        PhoneNumber = i.PhoneNumber,
                        Address = i.Address
                    }
                )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> ClientRemove(int ID)
        {
            await _eventiApi.DeleteClientAsync(ID);
            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> ClientDetails(int ID)
        {
            var response = await _eventiApi.GetClientAsync(ID);
            var entity = response.Content;
            var model = new ClientVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Username = entity.Username,
                Email = entity.Email,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber
            };

            return View(model);
        }

        public async Task<IActionResult> ClientEdit(int ID)
        {
            var response = await _eventiApi.GetClientAsync(ID);
            var entity = response.Content;
            var model = new ClientVM()
            {
                ID = entity.ID,
                AccountID = entity.AccountID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Username = entity.Username,
                Email = entity.Email,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber
            };

            return View(model);
        }

        public async Task<IActionResult> ClientSave(ClientVM model)
        {
            if(model.ID != 0)
            {
                var request = new ClientUpdateRequest()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Email = model.Email,
                    Address = model.LastName,
                    CreditCardNumber = model.CreditCardNumber
                };

                await _eventiApi.UpdateClientAsync(model.ID, request);
            }

            return Redirect("/Administrator/Home/Index");
        }
    }
}
