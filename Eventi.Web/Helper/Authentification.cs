using Eventi.Contracts.V1.Responses;
using Eventi.Sdk;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Eventi.Web.Helper
{
    public static class Authentification
    {
        private const string LoggedInUser = "logged_in_user";

        public static void SetLoggedInUser(this HttpContext context, AccountResponse account)
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
        public static async Task<AccountResponse> GetLoggedInUser(this HttpContext context)
        {
            IAuthApi authApi = context.RequestServices.GetService<IAuthApi>();
            string LoggedInUserCookie = context.Request.GetCookieJson<string>(LoggedInUser);

            if (LoggedInUserCookie != null)
            {
                var accountID = int.Parse(LoggedInUserCookie);
                var account = await authApi.GetAsync(accountID);

                return account.Content;
            }

            return null;
        }

        public static void RemoveCookie(this HttpContext context)
        {
            context.Response.RemoveCookie(LoggedInUser);
        }
    }
}
