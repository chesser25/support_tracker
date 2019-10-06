using support_tracker.Abstracts;
using System.Net.Mail;
using support_tracker.Models;

namespace support_tracker.Mailer
{
    public class TicketsMailer : ITicketsMailer
    {
        public void Send(Ticket ticket)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(ticket.CustomerEmail));
            mailMessage.Subject = ticket.Subject;
            mailMessage.Body = ticket.IssueDescription;
            smtpClient.Send(mailMessage);
        }
    }
}