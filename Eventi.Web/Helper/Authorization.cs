using Eventi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Eventi.Web.Helper
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute(AccountCategory AccountCategory) : base(typeof(EventiAuthorization))
        {
            Arguments = new object[] { AccountCategory };
        }
    }
    public class EventiAuthorization : IAsyncActionFilter
    {
        public EventiAuthorization(AccountCategory accountCategory)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var loggedInAccount = await context.HttpContext.GetLoggedInUser();

            if (loggedInAccount == null)
            {
                if (context.Controller is Controller controller)
                {
                    controller.TempData["error_message"] = "You did not log in";
                }
                context.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
                return;
            }

            await next();
            return;
        }
    }
}
