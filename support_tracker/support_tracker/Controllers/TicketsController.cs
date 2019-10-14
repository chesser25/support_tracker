﻿using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.Abstracts;
using System;
using Microsoft.AspNet.Identity;
using support_tracker.Auth;
using PagedList;
using support_tracker.ViewModels;
using System.Threading.Tasks;

namespace support_tracker.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IGenericRepository<Department> departmentsRepository;
        private readonly ITicketsRepository<Ticket> ticketsRepository;
        private readonly ITicketsMailer ticketsMailer;
        private readonly ITicketStatus<TicketStatus> ticketsStatusRepository;
        private readonly StaffManager staffManager;

        public TicketsController(IGenericRepository<Department> departmentsRepository, ITicketsRepository<Ticket> ticketsRepository, 
                                 ITicketsMailer ticketsMailer, ITicketStatus<TicketStatus> ticketsStatusRepository, AuthHelper authHelper)
        {
            this.departmentsRepository = departmentsRepository;
            this.ticketsRepository = ticketsRepository;
            this.ticketsMailer = ticketsMailer;
            this.ticketsStatusRepository = ticketsStatusRepository;
            this.staffManager = authHelper.GetStaffManagerFromOwinContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(this.departmentsRepository.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        // Method to get tickets list by sorting, search string value and page number
        [HttpGet]
        public ViewResult GetTickets(string sortOrder, string currentFilter, string searchString, string tab, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CustomerName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TicketStatus = sortOrder == "TicketStatus" ? "status_desc" : "TicketStatus";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.Tab = tab;
            var tickets = ticketsRepository.GetAll();
            tickets = ticketsRepository.GetTicketsBySearchString(searchString, tickets);
            tickets = ticketsRepository.GetTicketsBySort(sortOrder, tickets);
            tickets = ticketsRepository.GetTicketsByTab(tab, tickets, User.Identity.GetUserId());

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View("TicketsList", tickets.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetTicket(int id)
        {
            ViewBag.Statuses = new SelectList(this.ticketsStatusRepository.GetAll(), "TicketStatusId", "Status");
            var ticket = ticketsRepository.Get(id);
            return View("ShowTicket", ticket);
        }

        [HttpPost]
        public async Task<PartialViewResult> AssignTicket(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var ticket = ticketsRepository.Get(id);
                string userId = User.Identity.GetUserId();
                ticket.StaffMember = await staffManager.FindByIdAsync(userId);
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
                ticket.StaffMember = null;
                ticketsRepository.Update(ticket);
                return PartialView("TakeTicketPartial", ticket);
            }
            return null;
        }

        [HttpPost]
        public PartialViewResult ChangeTicketStatus(Ticket model)
        {
            if (Request.IsAjaxRequest())
            {
                var ticket = ticketsRepository.Get(model.TicketId);
                ticket.Status = ticketsStatusRepository.GetById(model.TicketStatusId);
                ticketsRepository.Update(ticket);
                return PartialView("AlertPartial", new AlertModel { Message = Constants_files.Constants.TICKET_STATUS_CHANGED, Style = Style.success.ToString() });
            }
            return null;
        }
    }
}