using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace support_tracker.Models
{
    public class Ticket
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int TicketId { get; set; }

        [Required]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string CustomerEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [Display(Name = "Issue description")]
        public string IssueDescription { get; set; }

        // Foreign keys 
        public int TicketStatusId { get; set; }
        public TicketStatus Status { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string StaffMemberId { get; set; }

        [ForeignKey("StaffMemberId")]
        public virtual StaffMember StaffMember { get; set; }
    }
}