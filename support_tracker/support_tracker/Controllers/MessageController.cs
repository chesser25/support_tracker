using System.Web.Mvc;
using support_tracker.Abstracts;
using support_tracker.Models;
using System.Linq;

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

        public ActionResult GetMessages(int ticketId)
        {
            var ticket = ticketsRepository.Get(ticketId);
            var messages = ticket.Messages?.ToList();
            return View("MessagePartial", messages);
        }
    }
}