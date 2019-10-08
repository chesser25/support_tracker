using support_tracker.Models;

namespace support_tracker.Abstracts
{
    public interface ITicketsMailer
    {
        void Send(string subject, string body, string userEmail);
    }
}
