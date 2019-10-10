using System;
using System.ComponentModel.DataAnnotations;
using support_tracker.Constants_files;

namespace support_tracker.Models
{
    public class Message
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = Constants.MESSAGE_MAX_SYMBOLS_COUNT)]
        [Required(ErrorMessage = Constants.FIELD_REQUIRED)]
        public string Text { get; set; }

        public DateTime CreationDate { get; set; }

        // Foreign keys
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }
    }
}