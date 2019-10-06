﻿using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.Abstracts;

namespace support_tracker.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IGenericRepository<Department> departmentsRepository;
        private readonly ITicketsRepository<Ticket> ticketsRepository;
        private readonly ITicketsMailer ticketsMailer;

        public TicketsController(IGenericRepository<Department> departmentsRepository, ITicketsRepository<Ticket> ticketsRepository, ITicketsMailer ticketsMailer)
        {
            this.departmentsRepository = departmentsRepository;
            this.ticketsRepository = ticketsRepository;
            this.ticketsMailer = ticketsMailer;
        }

        [HttpGet]
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
                ticketsRepository.Create(ticket);
                ticketsMailer.Send(ticket);
                return Redirect("/");
            }
            return View(ticket);
        }
    }
}