using support_tracker.Models;

namespace support_tracker.AbstractRepos
{
    public interface ITicketsRepository<T> : IGenericRepository<T>
    {
        void Create(T ticket);
    }
}