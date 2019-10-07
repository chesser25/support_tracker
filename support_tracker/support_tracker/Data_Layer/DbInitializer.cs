using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;


namespace support_tracker.DbLayer
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // Initialize departments
            IList<Department> departments = new List<Department>()
            {
                new Department { DepartmentName = "Technicians" },
                new Department { DepartmentName = "Repairmans" },
                new Department { DepartmentName = "Support" }
            };
            context.Departments.AddRange(departments);

            // Initialize ticket statuses
            IList<TicketStatus> ticketStatuses = new List<TicketStatus>()
            {
                new TicketStatus { Status = "Waiting for Staff Response" },
                new TicketStatus { Status = "Waiting for Customer" },
                new TicketStatus { Status = "On Hold" },
                new TicketStatus { Status = "Cancelled" },
                new TicketStatus { Status = "Completed" }
            };
            context.TicketStatuses.AddRange(ticketStatuses);
            base.Seed(context);
        }
    }
}