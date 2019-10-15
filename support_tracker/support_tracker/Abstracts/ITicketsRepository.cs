using support_tracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace support_tracker.Abstracts
{
    public interface ITicketsRepository<T> : IGenericRepository<T>
    {
        Task Create(T ticket);
        Task<T> Get(int id);
        Task Update(T ticket);
        IEnumerable<T> GetTicketsBySearchString(string searchString, IEnumerable<T> tickets);
        IEnumerable<T> GetTicketsBySort(string sortOrder, IEnumerable<T> tickets);
        IEnumerable<T> GetTicketsByTab(string tab, IEnumerable<T> tickets, string userId);
    }
}