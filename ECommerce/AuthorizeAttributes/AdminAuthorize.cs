using ECommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace ECommerce.AuthorizeAttributes
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public IUserService userService { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userToken = Convert.ToString(httpContext.Session["AuthToken"]);
            if (!string.IsNullOrEmpty(userToken))
            {
                authorize = userService.DoLogin(userToken);                
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Admin" },
                    { "action", "Login" }
               });
        }
    }
}