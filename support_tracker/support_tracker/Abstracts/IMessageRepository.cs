using System.Threading.Tasks;

namespace support_tracker.Abstracts
{
    public interface IMessageRepository<T> : IGenericRepository<T>
    {
        Task Create(T message);
    }
}
