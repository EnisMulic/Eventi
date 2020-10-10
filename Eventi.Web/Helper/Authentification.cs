using AutoMapper;
using Eventi.Contracts.V1.Responses;
using Eventi.Sdk;
using Eventi.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Eventi.Web.Helper
{
    public static class Authentification
    {
        private const string LoggedInUser = "logged_in_user";

        public static void SetLoggedInUser(this HttpContext context, Account account)
        {
            if (account == null)
            {
                context.Response.SetCookieJson(LoggedInUser, 0);
            }
            else
            {                                    
                context.Response.SetCookieJson(LoggedInUser, account.ID);
            }
        }
        public static async Task<Account> GetLoggedInUser(this HttpContext context)
        {
            IAuthApi authApi = context.RequestServices.GetService<IAuthApi>();
            IMapper mapper = context.RequestServices.GetService<IMapper>();

            string LoggedInUserCookie = context.Request.GetCookieJson<string>(LoggedInUser);

            if (LoggedInUserCookie != null)
            {
                var accountID = int.Parse(LoggedInUserCookie);
                var account = await authApi.GetAsync(accountID);

                return mapper.Map<Account>(account.Content);
            }

            return null;
        }

        public static void RemoveCookie(this HttpContext context)
        {
            context.Response.RemoveCookie(LoggedInUser);
        }
    }
}
