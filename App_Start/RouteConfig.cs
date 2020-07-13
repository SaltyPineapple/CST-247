using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Registration
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Default map route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Map route for register page
            routes.MapRoute(
                name: "Register",
                url: "{register}",
                defaults: new { controller ="register", action ="Index", id = UrlParameter.Optional }
            );

            // Map Route for Login page
            routes.MapRoute(
                name: "Login",
                url: "{login}",
                defaults: new { controller="Login", action="Index", id=UrlParameter.Optional }
            );
        }
    }
}
