using System.Data.Entity;
using support_issue_tracker.Models;

namespace support_issue_tracker.DbLayer
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}