using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace support_tracker.Models
{
    public class StaffMember : IdentityUser
    {
        public int StaffMemberId { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}