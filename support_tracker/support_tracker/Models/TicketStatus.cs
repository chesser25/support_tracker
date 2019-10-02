using System.Collections.Generic;

namespace support_tracker.Models
{
    public class TicketStatus
    {
        public int TicketStatusId { get; set; }
        public string Status { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}