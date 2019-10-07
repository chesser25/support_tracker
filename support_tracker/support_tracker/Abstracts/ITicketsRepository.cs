﻿using support_tracker.Models;

namespace support_tracker.Abstracts
{
    public interface ITicketsRepository<T> : IGenericRepository<T>
    {
        void Create(T ticket);
    }
}