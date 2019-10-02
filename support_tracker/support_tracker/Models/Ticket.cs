using System;
using System.Collections.Generic;

namespace support_issue_tracker.Models
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Subject { get; set; }
        public string IssueDescription { get; set; }

        // Foreign keys 
        public int TicketStatusId { get; set; }
        public TicketStatus Status { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}