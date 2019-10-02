using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace support_issue_tracker.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}