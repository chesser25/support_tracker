using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public virtual IEnumerable<T> GetAll()
        {
            var tickets = dbSet.Include(d => d.Department).Include(s => s.Status).Include(u => u.StaffMember).Include(c => c.Messages);
            return tickets;
        }

        public virtual void Create(T item)
        {
            dbSet.Add(item);
            dataContext.SaveChanges();
        }

        public virtual T Get(int id)
        {
            var ticket = dbSet.Find(id);
            return ticket;
        }

        public virtual void Update(T ticket)
        {
            dbSet.Attach(ticket);
            dataContext.Entry(ticket).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public virtual IEnumerable<T> GetTicketsBySearchString(string searchString, IEnumerable<T> tickets)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return tickets.Where(t => t.TicketHash.Contains(searchString) || t.Subject.Contains(searchString));
            }
            return tickets;
        }

        public virtual IEnumerable<T> GetTicketsBySort(string sortOrder, IEnumerable<T> tickets)
        {
            switch (sortOrder)
            {
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
                    tickets = tickets.OrderBy(t => t.CustomerName);
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