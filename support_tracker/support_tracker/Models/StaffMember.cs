using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace support_tracker.Models
{
    public class StaffMember : IdentityUser
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}