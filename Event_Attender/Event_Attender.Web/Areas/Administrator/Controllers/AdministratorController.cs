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

        public bool IsUsernameUnique(string Username)
        {
            List<LogPodaci> logPodaci = uow.LogPodaciRepository.GetAll().ToList();
            foreach (LogPodaci l in logPodaci)
                if (l.Username == Username)
                    return false;

            return true;
        }

        public bool IsEmailUnique(string Email)
        {
            List<LogPodaci> logPodaci = uow.LogPodaciRepository.GetAll().ToList();
            foreach (LogPodaci l in logPodaci)
                if (l.Email == Email)
                    return false;

            return true;
        }
    }
}