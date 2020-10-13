using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Data.EF;
using Eventi.Data.Models;
using Eventi.Data.Repository;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public AdministratorController(MojContext context, IEventiApi eventiApi)
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

        //public IActionResult PromijeniPassword(int id)
        //{
        //    var Admin = uow.AdministratorRepository.GetAll()
        //        .Include(i => i.Osoba)
        //        .Include(i => i.Osoba.LogPodaci)
        //        .Where(i => i.Id == id)
        //        .SingleOrDefault();

        //    var model = new AdministratorVM
        //    {
        //        ID = id,
        //        LogPodaciId = Admin.Osoba.LogPodaci.Id,
        //        Username = Admin.Osoba.LogPodaci.Username,
        //        OldPassword = Admin.Osoba.LogPodaci.Password
        //    };

        //    return View(model);
        //}


        //public IActionResult SnimiPassword(AdministratorVM model)
        //{
        //    var LogPodaci = uow.LogPodaciRepository.Get(model.LogPodaciId);
        //    LogPodaci.Password = model.NewPassword;
        //    ctx.SaveChanges();
        //    return Redirect("AdminProfil?id=" + model.ID);
        //}      
    }
}