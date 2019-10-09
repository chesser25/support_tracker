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
               "ticket/assign",
               url: "ticket/assign/{id}",
               defaults: new { controller = "Tickets", action = "AssignTicket", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "ticket/unassign",
               url: "ticket/unassign/{id}",
               defaults: new { controller = "Tickets", action = "UnassignTicket", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               "tickets/get",
               url: "tickets/get/{sortOrder}/{searchString}",
               defaults: new { controller = "Tickets", action = "GetTickets", sortOrder = UrlParameter.Optional, searchString = UrlParameter.Optional }
            );

            routes.MapRoute(
               "tickets/get/id",
               url: "tickets/get/{id}",
               defaults: new { controller = "Tickets", action = "GetTicket", id = UrlParameter.Optional }
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
