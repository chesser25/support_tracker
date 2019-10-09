using System;

namespace support_tracker.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        // Foreign keys
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }
    }
}