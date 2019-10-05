using System.Collections.Generic;

namespace support_tracker.AbstractRepos
{
    interface IGenericRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
    }
}