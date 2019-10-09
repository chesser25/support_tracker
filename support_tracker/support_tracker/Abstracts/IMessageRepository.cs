namespace support_tracker.Abstracts
{
    public interface IMessageRepository<T> : IGenericRepository<T>
    {
        void Create(T message);
    }
}
