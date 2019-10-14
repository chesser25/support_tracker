using support_tracker.Abstracts;
using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

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
        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}