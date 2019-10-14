using System.Collections.Generic;
using System.Threading.Tasks;

namespace support_tracker.Abstracts
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}