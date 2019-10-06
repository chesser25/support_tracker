using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using support_tracker.Abstracts;

namespace support_tracker.Repositories
{
    public class DepartmentsRepository<T,C> : IGenericRepository<T>
        where T : class
        where C : DbContext
    {
        private C dataContext;
        private DbSet<T> dbSet;

        public DepartmentsRepository(C context)
        {
            this.dataContext = context;
            this.dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
    }
}