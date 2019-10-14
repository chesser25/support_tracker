﻿using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.Abstracts;
using System;
using Microsoft.AspNet.Identity;
using support_tracker.Auth;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using support_tracker.ViewModels;

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
            IEnumerable<Ticket> tickets = ticketsRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(t => t.TicketHash.Contains(searchString) || t.Subject.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tickets = tickets.OrderByDescending(t => t.CustomerName);
                    break;
                case "TicketStatus":
                    tickets = tickets.OrderBy(t => t.Status.Status);
                    break;
                case "status_desc":
                    tickets = tickets.OrderByDescending(t => t.Status.Status);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.CustomerName);
                    break;
            }

            ViewBag.Tab = tab;
            switch (tab)
            {
                case "opened":
                    tickets = tickets.Where(t => t.Status.Status.Equals("Waiting for Staff Response") || t.Status.Status.Equals("Waiting for Customer"));
                    break;
                case "on_hold":
                    tickets = tickets.Where(t => t.Status.Status.Equals("On Hold"));
                    break;
                case "closed":
                    tickets = tickets.Where(t => t.Status.Status.Equals("Cancelled") || t.Status.Status.Equals("Completed"));
                    break;
                case "my_tickets":
                    tickets = tickets.Where(t => t.StaffMemberId == User.Identity.GetUserId());
                    break;
                case "unassigned":
                    tickets = tickets.Where(t => t.StaffMember == null);
                    break;
            }

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
        public PartialViewResult AssignTicket(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var ticket = ticketsRepository.Get(id);
                ticket.StaffMember = staffManager.FindById(User.Identity.GetUserId());
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