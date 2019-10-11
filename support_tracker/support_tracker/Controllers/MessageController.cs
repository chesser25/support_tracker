using System.Web.Mvc;
using support_tracker.Abstracts;
using support_tracker.Models;
using System.Linq;
using System;
using Microsoft.AspNet.Identity;
using support_tracker.Auth;

namespace support_tracker.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository<Message> messageRepository;
        private readonly ITicketsRepository<Ticket> ticketsRepository;
        private readonly ITicketsMailer ticketsMailer;
        private readonly StaffManager staffManager;

        public MessageController(IMessageRepository<Message> messageRepository, ITicketsRepository<Ticket> ticketsRepository, ITicketsMailer ticketsMailer, AuthHelper authHelper)
        {
            this.messageRepository = messageRepository;
            this.ticketsRepository = ticketsRepository;
            this.ticketsMailer = ticketsMailer;
            this.staffManager = authHelper.GetStaffManagerFromOwinContext;
        }

        [HttpGet]
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult GetMessages(int ticketId)
        {
            var ticket = ticketsRepository.Get(ticketId);
            var messages = ticket.Messages?.ToList();
            ViewBag.TicketId = ticketId;
            return View(messages);
        }

        [HttpGet]
        [ChildActionOnly]
        [AllowAnonymous]
        public ActionResult CreateMessage(int ticketId)
        {
            ViewBag.TicketId = ticketId;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateMessage(Message message)
        {
            if(ModelState.IsValid)
            {
                var ticket = ticketsRepository.Get(message.TicketId);
                var user = staffManager.FindById(User.Identity.GetUserId());
                message.CreationDate = DateTime.Now;
                message.Ticket = ticket;
                message.StaffMember = user;
                messageRepository.Create(message);
                ticketsMailer.Send(Constants_files.Constants.MAIL_HEADER, string.Format("{0} Link: {1}", Constants_files.Constants.SUPPORT_RESPONSE_ON_TICKET, Url.Action("GetTicket", "Tickets", new { id = ticket.TicketId }, Request.Url.Scheme)), ticket.CustomerEmail);
            }
            else
            {
                TempData["error"] = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
            }
            return RedirectToRoute("tickets/get/id", new { id = message.TicketId });
        }
    }
}