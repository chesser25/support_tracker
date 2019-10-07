namespace support_tracker.Abstracts
{
    public interface ITicketStatus<T>: IGenericRepository<T>
    {
        T GetFirst();
    }
}