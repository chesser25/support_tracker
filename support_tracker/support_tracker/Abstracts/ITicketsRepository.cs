using support_tracker.Models;
using System.Collections.Generic;

namespace support_tracker.Abstracts
{
    public interface ITicketsRepository<T> : IGenericRepository<T>
    {
        void Create(T ticket);
        T Get(int id);
        void Update(T ticket);
        IEnumerable<T> GetTicketsBySearchString(string searchString, IEnumerable<T> tickets);
        IEnumerable<T> GetTicketsBySort(string sortOrder, IEnumerable<T> tickets);
        IEnumerable<T> GetTicketsByTab(string tab, IEnumerable<T> tickets, string userId);
    }
}