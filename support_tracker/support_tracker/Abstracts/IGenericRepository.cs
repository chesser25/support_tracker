using System.Collections.Generic;

namespace support_tracker.Abstracts
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}