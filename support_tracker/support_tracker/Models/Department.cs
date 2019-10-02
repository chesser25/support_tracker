using System.Collections.Generic;

namespace support_tracker.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}