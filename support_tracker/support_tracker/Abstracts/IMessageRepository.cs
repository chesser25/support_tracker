namespace support_tracker.Abstracts
{
    interface IMessageRepository<T> : IGenericRepository<T>
    {
        void Create(T message);
    }
}
