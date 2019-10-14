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
               "message/create",
               url: "message/create/{ticketId}",
               defaults: new { controller = "Messages", action = "CreateMessage", ticketId = UrlParameter.Optional }
            );

            routes.MapRoute(
               "messages/getAll",
               url: "messages/getAll/{ticketId}",
               defaults: new { controller = "Messages", action = "GetMessages", ticketId = UrlParameter.Optional }
            );

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
               "dashboard",
               url: "tickets/getAll/{sortOrder}/{currentFilter}/{searchString}/{tab}/{page}",
               defaults: new { controller = "Tickets", action = "GetTickets", sortOrder = UrlParameter.Optional, currentFilter = UrlParameter.Optional,  searchString = UrlParameter.Optional, tab = UrlParameter.Optional, page = UrlParameter.Optional }
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
              "ticket/create",
              url: "ticket/create",
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
