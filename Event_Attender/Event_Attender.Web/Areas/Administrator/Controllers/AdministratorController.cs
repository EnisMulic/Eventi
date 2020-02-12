using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Event_Attender.Data.Repository;
using Event_Attender.Web.Areas.Administrator.Models;
using Event_Attender.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Attender.Web.Areas.Administrator.Controllers
{
    [Autorizacija(korisnik: false, organizator: false, administrator: true, radnik: false)]
    [Area("Administrator")]
    public class AdministratorController : Controller
    {
        private readonly MojContext ctx;
        private readonly EventAttenderUnitOfWork uow;
        public AdministratorController(MojContext context)
        {
            ctx = context;
            uow = new EventAttenderUnitOfWork(ctx);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult AdminProfil(int Id)
        {
            AdministratorVM model = uow.AdministratorRepository.GetAll()
                .Select
                (
                    i => new AdministratorVM
                    {
                        Id          = i.Id,
                        Ime         = i.Osoba.Ime,
                        Prezime     = i.Osoba.Prezime,
                        LogPodaciId = i.Osoba.LogPodaci.Id,
                        Email       = i.Osoba.LogPodaci.Email,
                        Username    = i.Osoba.LogPodaci.Username,
                        Password    = i.Osoba.LogPodaci.Password,
                        Telefon     = i.Osoba.Telefon,
                        Grad        = i.Osoba.Grad.Naziv
                        
                    }
                )
                .Where(i => i.Id == Id)
                .SingleOrDefault();

            return View(model);
        }

        public IActionResult Uredi(int id)
        {
            return AdminProfil(id);
        }

        public IActionResult Snimi(AdministratorVM model)
        {
            var Admin = uow.AdministratorRepository.GetAll()
                .Include(i => i.Osoba)
                .Include(i => i.Osoba.LogPodaci)
                .Where(i => i.Id == model.Id)
                .SingleOrDefault();

            Admin.Osoba.Ime = model.Ime;
            Admin.Osoba.Prezime = model.Prezime;
            Admin.Osoba.LogPodaci.Email = model.Email;
            Admin.Osoba.LogPodaci.Username = model.Username;
            Admin.Osoba.Telefon = model.Telefon;

            ctx.SaveChanges();
            return Redirect("AdminProfil?id=" + model.Id);
        }

        public IActionResult PromijeniPassword(int id)
        {
            var Admin = uow.AdministratorRepository.GetAll()
                .Include(i => i.Osoba)
                .Include(i => i.Osoba.LogPodaci)
                .Where(i => i.Id == id)
                .SingleOrDefault();

            var model = new AdministratorVM
            {
                Id = id,
                LogPodaciId = Admin.Osoba.LogPodaci.Id,
                Username = Admin.Osoba.LogPodaci.Username,
                OldPassword = Admin.Osoba.LogPodaci.Password
            };

            return View(model);
        }


        public IActionResult SnimiPassword(AdministratorVM model)
        {
            var LogPodaci = uow.LogPodaciRepository.Get(model.LogPodaciId);
            LogPodaci.Password = model.NewPassword;
            ctx.SaveChanges();
            return Redirect("AdminProfil?id=" + model.Id);
        }

        public bool IsUsernameUnique(string Username, int LogPodaciId)
        {
            List<LogPodaci> logPodaci = uow.LogPodaciRepository.GetAll().ToList();
            foreach (LogPodaci l in logPodaci)
                if (l.Username == Username && l.Id != LogPodaciId)
                    return false;

            return true;
        }

        public bool IsEmailUnique(string Email, int LogPodaciId)
        {
            List<LogPodaci> logPodaci = uow.LogPodaciRepository.GetAll().ToList();
            foreach (LogPodaci l in logPodaci)
                if (l.Email == Email && l.Id != LogPodaciId)
                    return false;

            return true;
        }

        public bool IsOldPassword(string OldPassword, int LogPodaciId) => 
            OldPassword == uow.LogPodaciRepository.Get(LogPodaciId).Password;

        public bool MatchNewPassword(string NewPasswordConfirmed, string NewPassword) =>
            NewPasswordConfirmed == NewPassword;
    }
}