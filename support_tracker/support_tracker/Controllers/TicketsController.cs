using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.Abstracts;
using System;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using support_tracker.Auth;
using Microsoft.AspNet.Identity;

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

        private StaffManager StaffManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<StaffManager>();
            }
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
                ticket.TicketStatusId = ticketsStatusRepository.GetFirst().TicketStatusId;
                StaffMember staffMember = StaffManager.FindById(User.Identity.GetUserId());
                ticket.StaffMemberId = User.Identity.GetUserId();
                ticketsRepository.Create(ticket);
                ticketsMailer.Send(ticket);
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
    }
}