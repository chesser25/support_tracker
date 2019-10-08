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
            return dbSet.Include(d => d.Department).Include(s => s.Status).Include(u => u.StaffMember).Include(c => c.Comments).ToList();
        }

        public virtual void Create(T item)
        {
            dbSet.Add(item);
            dataContext.SaveChanges();
        }

        public virtual T Get(int id)
        {
            return dbSet.Where(t => t.TicketId == id)?.Include(d => d.Department).Include(s => s.Status).Include(u => u.StaffMember).Include(c => c.Comments).FirstOrDefault();
        }
    }
}