using System.Web.Mvc;
using support_tracker.Abstracts;
using support_tracker.Models;

namespace support_tracker.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository<Message> messageRepository;

        public MessageController(IMessageRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public PartialViewResult GetMessages()
        {
            if (Request.IsAjaxRequest())
            {
                var messages = messageRepository.GetAll();
                return PartialView("MessagePartial", messages);
            }
            return null;
        }
    }
}