using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool korisnik, bool organizator, bool administrator, bool radnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { korisnik, organizator, administrator, radnik };
        }
    }
    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool korisnik, bool organizator, bool administrator, bool radnik)
        {
            _korisnik = korisnik;
            _organizator = organizator;
            _administrator = administrator;
            _radnik = radnik;
        }
        private readonly bool _korisnik;
        private readonly bool _organizator;
        private readonly bool _administrator;
        private readonly bool _radnik;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            LogPodaci logPodaci = context.HttpContext.GetLogiraniUser();

           
            if (logPodaci==null)
            {
                if(context.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste se logirali";
                }
                context.Result = new RedirectToActionResult("Index", "Prijava", new { @area = "" });
                return;  
            }

            MojContext ctx = context.HttpContext.RequestServices.GetService<MojContext>();
            

            if (_korisnik && ctx.Korisnik.Where(k => k.Osoba.LogPodaciId == logPodaci.Id).Any())
            {
                await next();
                return;
            }

            if (_organizator && ctx.Organizator.Where(o=>o.LogPodaciId==logPodaci.Id).Any())
            {
                await next();
                return;
            }

            if (_administrator && ctx.Administrator.Where(a=>a.Osoba.LogPodaciId==logPodaci.Id).Any())
            {
                await next();
                return;
            }
            if (_radnik && ctx.Korisnik.Where(r=>r.Osoba.LogPodaciId==logPodaci.Id).Any())
            {
                await next();
                return;
            }

           
        }
    }
}
