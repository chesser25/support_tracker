using System.Web.Mvc;
using System.Web.Routing;

namespace support_tracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "tickets/get",
               url: "tickets/get",
               defaults: new { controller = "Tickets", action = "GetTickets" }
            );

            routes.MapRoute(
               "login",
               url: "login",
               defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              "register",
               url: "register",
               defaults: new { controller = "Accounts", action = "Register" }
            );

            routes.MapRoute(
              "tickets/create",
              url: "tickets/create",
              defaults: new { controller = "Tickets", action = "Create" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
