using support_tracker.Abstracts;
using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace support_tracker.Repositories
{
    public class TicketStatusRepository<T, C> : ITicketStatus<T>
        where T : TicketStatus
        where C : DbContext
    {
        private C dataContext;
        private DbSet<T> dbSet;

        public TicketStatusRepository(C context)
        {
            this.dataContext = context;
            this.dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetFirst()
        {
            return dbSet.FirstOrDefault<T>();
        }

        public T GetById(int id)
        {
            return dbSet.Where(t => t.TicketStatusId == id).FirstOrDefault();
        }
    }
}