using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClassStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index"
                }
            );

           
            routes.MapRoute(null, "Page{page}", new
            {
                controller = "Class",
                action = "List",
                Category = (string)null
            },
            new { page = @"\d+" });

            routes.MapRoute(null, "{Category}/Page{page}", new
            {
                controller = "Class",
                action = "List"
            },
            new { page = @"\d+" });

            routes.MapRoute(null, "{Category}", new
            {
                controller = "Class",
                action = "List",
                Page = 1
            });
        }
    }
}  