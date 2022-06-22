using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string? Roles { get; set; } = null;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var visitor = context.HttpContext.Request.Headers["Visitor"];
            if (visitor == "employee")
            {
                var user = (User?)context.HttpContext.Items["User"];
                if (user == null)
                {
                    context.Result = new JsonResult(new { message = "UnAuthorized" })
                    { StatusCode = StatusCodes.Status401Unauthorized };
                }
                else
                {
                    if (Roles != null)
                    {
                        var roles = Roles.Split(", ");

                        if (Array.Exists(roles, role => role == user.Role))
                        {
                        }
                        else
                        {
                            context.Result = new JsonResult(new { message = "doesn't get access with this role" })
                            { StatusCode = StatusCodes.Status403Forbidden };
                        }
                    }
                }
            }
            else
            {
                var customer = (Customer?)context.HttpContext.Items["User"];
                if (customer != null && Roles == null)
                {
                }
                else
                {
                    context.Result = new JsonResult(new { message = "UnAuthorized" })
                    { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
