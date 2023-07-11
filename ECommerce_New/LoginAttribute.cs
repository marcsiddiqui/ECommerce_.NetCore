using Azure.Core;
using ECommerce_New.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ECommerce_New
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!CheckIfLoggedIn(context))
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Auth"
                });
                context.Result = new RedirectToRouteResult(values);
            }
        }

        private bool CheckIfLoggedIn(ActionExecutedContext context)
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
