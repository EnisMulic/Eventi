using Event_Attender.Data.EF;
using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniUser = "logirani_user";

        public static void SetLogiraniUser(this HttpContext context, LogPodaci logPodaci)
        {
            if (logPodaci == null)
            {
                context.Response.SetCookieJson(LogiraniUser, 0);
            }
            else
            {                                     // logPodaci?
                context.Response.SetCookieJson(LogiraniUser, logPodaci.Id);
            }
        }
        public static /*int*/ LogPodaci GetLogiraniUser(this HttpContext context)
        {
            MojContext ctx = context.RequestServices.GetService<MojContext>();
            //MojContext ctx = new MojContext();

            string logPodaciIdCookie = context.Request.GetCookieJson<string>(LogiraniUser);

            if (logPodaciIdCookie == null)
                return null;


            LogPodaci l = ctx.LogPodaci.Where(l => l.Id == int.Parse(logPodaciIdCookie)).SingleOrDefault();
           
            return l;

            //return int.Parse(logPodaciIdCookie);  // verzija1
        }

        public static void RemoveCookie(this HttpContext context)
        {
            context.Response.RemoveCookie(LogiraniUser);
        }
    }
}
