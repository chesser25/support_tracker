using support_tracker.Abstracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace support_tracker.Repositories
{
    public class TicketStatusRepository<T, C> : ITicketStatus<T>
        where T : class
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
    }
}