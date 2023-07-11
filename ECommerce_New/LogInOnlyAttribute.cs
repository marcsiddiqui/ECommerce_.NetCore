using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce_New
{
    public class LogInOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsCustomerLoggedIn(context))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Auth"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        public bool IsCustomerLoggedIn(ActionExecutingContext context)
        {
            if (context == null)
            {
                return false;
            }

            if ((context.HttpContext.Request.Cookies["AuthenticatedCustomer"] != null ? 
                Convert.ToInt32(context.HttpContext.Request.Cookies["AuthenticatedCustomer"]) : 0) <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
