using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Accessor
    {
    public class RouteConfig
        {
        public static void RegisterRoutes(RouteCollection routes)
            {
            routes.IgnoreRoute ("{resource}.axd/{*pathInfo}");

            routes.MapRoute (
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute (
                name: "Challenges",
                url: "Challenges/{id}",
                defaults: new { controller = "Challenges", action = "Get", id = UrlParameter.Optional });
            routes.MapRoute (
                name: "Repository",
                url: "Repository/{id}",
                defaults: new { controller = "Repository", action = "Get", id = UrlParameter.Optional });
            }
        }
    }
