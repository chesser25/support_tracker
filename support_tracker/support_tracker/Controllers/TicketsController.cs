using support_tracker.Models;
using System.Web.Mvc;

namespace support_tracker.Controllers
{
    public class TicketsController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            return View();
        }
    }
}