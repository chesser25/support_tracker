using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.Abstracts;
using System;
using Microsoft.AspNet.Identity;
using System.Web;
using support_tracker.Auth;
using Microsoft.AspNet.Identity.Owin;

namespace support_tracker.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IGenericRepository<Department> departmentsRepository;
        private readonly ITicketsRepository<Ticket> ticketsRepository;
        private readonly ITicketsMailer ticketsMailer;
        private readonly ITicketStatus<TicketStatus> ticketsStatusRepository;

        public TicketsController(IGenericRepository<Department> departmentsRepository, ITicketsRepository<Ticket> ticketsRepository, ITicketsMailer ticketsMailer, ITicketStatus<TicketStatus> ticketsStatusRepository)
        {
            this.departmentsRepository = departmentsRepository;
            this.ticketsRepository = ticketsRepository;
            this.ticketsMailer = ticketsMailer;
            this.ticketsStatusRepository = ticketsStatusRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(this.departmentsRepository.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            ViewBag.Departments = new SelectList(this.departmentsRepository.GetAll(), "DepartmentId", "DepartmentName");
            if (ModelState.IsValid)
            {
                ticket.TicketHash = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 6);
                ticket.TicketStatusId = ticketsStatusRepository.GetFirst().TicketStatusId;
                ticketsRepository.Create(ticket);
                ticketsMailer.Send(Constants_files.Constants.MAIL_HEADER, string.Format("{0} Ticket id: {1}. Ticket url: {2}", Constants_files.Constants.MAIL_SUBJECT, ticket.TicketHash, Url.Action("GetTicket", "Tickets", new { id = ticket.TicketId }, Request.Url.Scheme)), ticket.CustomerEmail);
                return Redirect("/");
            }
            return View(ticket);
        }

        [HttpGet]
        public ActionResult GetTickets()
        {
            var tickets = ticketsRepository.GetAll();
            return View("TicketsList", tickets);
        }

        [HttpGet]
        public ActionResult GetTicket(int id)
        {
            var ticket = ticketsRepository.Get(id);
            return View("ShowTicket", ticket);
        }

        [HttpPost]
        public PartialViewResult AssignTicket(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var ticket = ticketsRepository.Get(id);
                string userId = User.Identity.GetUserId();
                ticket.StaffMember = StaffManager.FindById(User.Identity.GetUserId());
                ticketsRepository.Update(ticket);
                return PartialView("TakeTicketPartial", ticket);
            }
            return null;
        }

        [HttpPost]
        public PartialViewResult UnassignTicket(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var ticket = ticketsRepository.Get(id);
                string userId = User.Identity.GetUserId();
                ticket.StaffMember = null;
                ticketsRepository.Update(ticket);
                return PartialView("TakeTicketPartial", ticket);
            }
            return null;
        }

        private StaffManager StaffManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<StaffManager>();
            }
        }
    }
}