using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eventi.Common;
using Eventi.Contracts.V1.Requests;
using Eventi.Sdk;
using Eventi.Web.Areas.Administrator.Models;
using Eventi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eventi.Web.Areas.Administrator.Controllers
{
    [Authorization(AccountCategory: AccountCategory.Administrator)]
    [Area("Administrator")]
    public class EventController : Controller
    {
        private readonly IEventiApi _eventiApi;
        public EventController(IEventiApi eventiApi)
        {
            _eventiApi = eventiApi;
        }
        public async Task<IActionResult> EventList()
        {
            var response = await _eventiApi.GetEventAsync();
            var model = response.Content.Data
                .Select
                (
                    e => new EventVM
                    {
                        ID = e.ID,
                        Name = e.Name,
                        Description = e.Description,
                        Start = e.Start,
                        End = e.End,
                        EventCategory = e.EventCategory,
                        IsApproved = e.IsApproved,
                        IsCanceled = e.IsCanceled,
                        Image = e.Image,
                        OrganizerID = e.OrganizerID,
                        AdministratorID = e.AdministratorID,
                        VenueID = e.VenueID
                    }
                 )
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> EventRemove(int ID)
        {
            await _eventiApi.DeleteEventAsync(ID);
            return Redirect("/Administrator/Home/Index");
        }

        public async Task<IActionResult> EventDetails(int ID)
        {
            var response = await _eventiApi.GetEventAsync(ID);
            var entity = response.Content;
            var model = new EventVM()
            {
                ID = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Start = entity.Start,
                End = entity.End,
                EventCategory = entity.EventCategory,
                Image = entity.Image,
                IsApproved = entity.IsApproved,
                IsCanceled = entity.IsCanceled,
                OrganizerID = entity.OrganizerID,
                AdministratorID = entity.AdministratorID,
                VenueID = entity.VenueID
            };

            return View(model);
        }


        public async Task<IActionResult> EventSave(EventVM model, IFormFile image)
        {
            String fileName = new String("");
            if (image != null && image.Length > 0)
            {
                fileName = Path.GetFileName(image.FileName);
                //var mappedPath = HttpContext.GetServerVariable.MapPath("~/Content/Images/");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\items", fileName);
                using (var fajlSteam = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fajlSteam);
                }
            }

            if(model.ID == 0)
            {
                var insertRequest = new EventInsertRequest()
                {
                    Name = model.Name,
                    Description = model.Description,
                    EventCategory = model.EventCategory,
                    Start = model.Start,
                    End = model.Start,
                    IsApproved = model.IsApproved,
                    IsCanceled = model.IsCanceled,
                    OrganizerID = model.OrganizerID,
                    VenueID = model.VenueID,
                    Image = fileName
                };

                await _eventiApi.CreateEventAsync(insertRequest);
            }
            else
            {
                var updateRequest = new EventUpdateRequest()
                {
                    Name = model.Name,
                    Description = model.Description,
                    EventCategory = model.EventCategory,
                    Start = model.Start,
                    End = model.Start,
                    IsApproved = model.IsApproved,
                    IsCanceled = model.IsCanceled,
                    OrganizerID = model.OrganizerID,
                    AdministratorID = model.AdministratorID,
                    VenueID = model.VenueID,
                    Image = fileName
                };

                await _eventiApi.UpdateEventAsync(model.ID, updateRequest);
            }

            return Redirect("Index");
        }

        public async Task<IActionResult> EventEdit(int ID)
        {
            var response = await _eventiApi.GetEventAsync(ID);
            var entity = response.Content;
            var model = new EventVM()
            {
                ID = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Start = entity.Start,
                End = entity.End,
                EventCategory = entity.EventCategory,
                Image = entity.Image,
                IsApproved = entity.IsApproved,
                IsCanceled = entity.IsCanceled,
                OrganizerID = entity.OrganizerID,
                AdministratorID = entity.AdministratorID,
                VenueID = entity.VenueID
            };

            model.Organizers = (await _eventiApi.GetOrganizerAsync()).Content.Data.Select(
                d => new SelectListItem(d.Name, d.ID.ToString())).ToList();
            model.Administrators = (await _eventiApi.GetAdministratorAsync()).Content.Data.Select(
                d => new SelectListItem(d.FirstName + " " + d.LastName, d.ID.ToString())).ToList();
            model.Venues = (await _eventiApi.GetVenueAsync()).Content.Data.Select(
                d => new SelectListItem(d.Name, d.ID.ToString())).ToList();

            return View(model);
        }

        public async Task<IActionResult> EventCreate()
        {
            var model = new EventVM();
            model.Organizers = (await _eventiApi.GetOrganizerAsync()).Content.Data.Select(
                d => new SelectListItem(d.Name, d.ID.ToString())).ToList();
            model.Administrators = (await _eventiApi.GetAdministratorAsync()).Content.Data.Select(
                d => new SelectListItem(d.FirstName + " " + d.LastName, d.ID.ToString())).ToList();
            model.Venues = (await _eventiApi.GetVenueAsync()).Content.Data.Select(
                d => new SelectListItem(d.Name, d.ID.ToString())).ToList();

            return View(model);
        }

    }
}
