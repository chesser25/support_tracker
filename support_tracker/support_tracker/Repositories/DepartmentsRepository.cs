using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using support_tracker.Abstracts;
using support_tracker.Models;

namespace support_tracker.Repositories
{
    public class DepartmentsRepository<T,C> : IGenericRepository<T>
        where T : Department
        where C : DbContext
    {
        private C dataContext;
        private DbSet<T> dbSet;

        public DepartmentsRepository(C context)
        {
            this.dataContext = context;
            this.dbSet = context.Set<T>();
        }
        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    }
}