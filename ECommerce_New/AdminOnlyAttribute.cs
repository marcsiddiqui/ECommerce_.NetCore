using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce_New
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!IsCustomerAdmin(context))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        public bool IsCustomerAdmin(ActionExecutingContext context)
        {
            if (context == null)
            {
                return false;
            }

            if ((context.HttpContext.Request.Cookies["LoginType"] != null ? 
                context.HttpContext.Request.Cookies["LoginType"] : "") != "Admin")
            {
                return false;
            }

            return true;
        }
    }
}
