using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using support_tracker.Abstracts;

namespace support_tracker.Repositories
{
    public class TicketsRepository<T,C> : ITicketsRepository<T>
        where T : class
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
            return dbSet.ToList();
        }

        public virtual void Create(T item)
        {
            dbSet.Add(item);
            dataContext.SaveChanges();
        }
    }
}