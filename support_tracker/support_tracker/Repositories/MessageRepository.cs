using support_tracker.Abstracts;
using support_tracker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public void Create(T message)
        {
            dbSet.Add(message);
            dataContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
    }
}