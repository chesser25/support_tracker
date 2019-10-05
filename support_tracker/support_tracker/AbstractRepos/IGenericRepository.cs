using System.Collections.Generic;

namespace support_tracker.AbstractRepos
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}