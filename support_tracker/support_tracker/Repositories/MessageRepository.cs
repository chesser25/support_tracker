using support_tracker.Abstracts;
using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace support_tracker.Repositories
{
    public class MessageRepository<T, C> : IMessageRepository<T>
        where T : Message
        where C : DbContext
    {
        private C dataContext;
        private DbSet<T> dbSet;

        public MessageRepository(C context)
        {
            this.dataContext = context;
            this.dbSet = context.Set<T>();
        }

        public async Task Create(T message)
        {
            dbSet.Add(message);
            await dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    }
}