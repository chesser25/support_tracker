using support_tracker.Abstracts;
using System.Net.Mail;
using support_tracker.Models;

namespace support_tracker.Mailer
{
    public class TicketsMailer : ITicketsMailer
    {
        public void Send(string subject, string body, string userEmail)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            smtpClient.Send(mailMessage);
        }
    }
}