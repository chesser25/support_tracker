using System.Data.Entity;
using support_tracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace support_tracker.DbLayer
{
    public class DataContext : IdentityDbContext<StaffMember>
    {
        public DataContext() : base()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}