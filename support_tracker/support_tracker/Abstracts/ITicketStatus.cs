using System.Threading.Tasks;

namespace support_tracker.Abstracts
{
    public interface ITicketStatus<T>: IGenericRepository<T>
    {
        Task<T> GetById(int id);
    }
}