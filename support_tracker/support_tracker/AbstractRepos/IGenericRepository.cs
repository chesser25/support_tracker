using System.Collections.Generic;

namespace support_tracker.AbstractRepos
{
    interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}