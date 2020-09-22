using Eventi.Data.EF;
using Eventi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Helper
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
            {                                    
                context.Response.SetCookieJson(LogiraniUser, logPodaci.Id);
            }
        }
        public static  LogPodaci GetLogiraniUser(this HttpContext context)
        {
            MojContext ctx = context.RequestServices.GetService<MojContext>();


            string logPodaciIdCookie = context.Request.GetCookieJson<string>(LogiraniUser);

            if (logPodaciIdCookie == null)
                return null;


            LogPodaci l = ctx.LogPodaci.Where(l => l.Id == int.Parse(logPodaciIdCookie)).SingleOrDefault();

            return l;


        }

        public static void RemoveCookie(this HttpContext context)
        {
            context.Response.RemoveCookie(LogiraniUser);
        }
    }
}
