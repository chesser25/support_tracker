using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using support_tracker.Abstracts;
using support_tracker.Models;

namespace support_tracker.Repositories
{
    public class TicketsRepository<T,C> : ITicketsRepository<T>
        where T: Ticket 
        where C : DbContext
    {
        private C dataContext;
        private DbSet<T> dbSet;

        public TicketsRepository(C context)
        {
            this.dataContext = context;
            this.dbSet = context.Set<T>();
        }
        public async virtual Task<IEnumerable<T>> GetAll()
        {
            var tickets = await dbSet.Include(d => d.Department).Include(s => s.Status).Include(u => u.StaffMember).Include(c => c.Messages).ToListAsync();
            return tickets;
        }

        public async virtual Task Create(T item)
        {
            dbSet.Add(item);
            await dataContext.SaveChangesAsync();
        }

        public async virtual Task<T> Get(int id)
        {
            var ticket = await dbSet.FindAsync(id);
            return ticket;
        }

        public async virtual Task Update(T ticket)
        {
            dbSet.Attach(ticket);
            dataContext.Entry(ticket).State = EntityState.Modified;
            await dataContext.SaveChangesAsync();
        }

        public virtual IEnumerable<T> GetTicketsBySearchString(string searchString, IEnumerable<T> tickets)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(t => t.TicketHash.Contains(searchString) || t.Subject.Contains(searchString));
            }
            return tickets;
        }

        public virtual IEnumerable<T> GetTicketsBySort(string sortOrder, IEnumerable<T> tickets)
        {
            switch (sortOrder)
            {
                case "index_desc":
                    tickets = tickets.OrderByDescending(t => t.TicketId);
                    break;
                case "CustomerName":
                    tickets = tickets.OrderBy(t => t.CustomerName);
                    break;
                case "name_desc":
                    tickets = tickets.OrderByDescending(t => t.CustomerName);
                    break;
                case "TicketStatus":
                    tickets = tickets.OrderBy(t => t.Status.Status);
                    break;
                case "status_desc":
                    tickets = tickets.OrderByDescending(t => t.Status.Status);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.TicketId);
                    break;
            }
            return tickets;
        }

        public virtual IEnumerable<T> GetTicketsByTab(string tab, IEnumerable<T> tickets, string userId)
        {
            switch (tab)
            {
                case "opened":
                    tickets = tickets.Where(t => t.Status.Status.Equals("Waiting for Staff Response") || t.Status.Status.Equals("Waiting for Customer"));
                    break;
                case "on_hold":
                    tickets = tickets.Where(t => t.Status.Status.Equals("On Hold"));
                    break;
                case "closed":
                    tickets = tickets.Where(t => t.Status.Status.Equals("Cancelled") || t.Status.Status.Equals("Completed"));
                    break;
                case "my_tickets":
                    tickets = tickets.Where(t => t.StaffMemberId == userId);
                    break;
                case "unassigned":
                    tickets = tickets.Where(t => t.StaffMember == null);
                    break;
            }
            return tickets;
        }
    }
}