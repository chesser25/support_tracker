using System.Web.Mvc;
using support_tracker.Abstracts;
using support_tracker.Models;
using System.Linq;
using System;
using Microsoft.AspNet.Identity;
using System.Web;
using support_tracker.Auth;
using Microsoft.AspNet.Identity.Owin;

namespace support_tracker.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository<Message> messageRepository;
        private readonly ITicketsRepository<Ticket> ticketsRepository;

        public MessageController(IMessageRepository<Message> messageRepository, ITicketsRepository<Ticket> ticketsRepository)
        {
            this.messageRepository = messageRepository;
            this.ticketsRepository = ticketsRepository;
        }


        private StaffManager StaffManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<StaffManager>();
            }
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
                var user = StaffManager.FindById(User.Identity.GetUserId());
                message.CreationDate = DateTime.Now;
                message.Ticket = ticket;
                message.StaffMember = user;
                messageRepository.Create(message);
            }
            else
            {
                TempData["error"] = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
            }
            return RedirectToRoute("tickets/get/id", new { id = message.TicketId });
        }
    }
}