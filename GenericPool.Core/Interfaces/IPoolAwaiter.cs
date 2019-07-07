using System;

namespace GenericPool.Interfaces
{
    public interface IPoolAwaiter
    {
        bool HasDelay { get; }
        void AddDelay(TimeSpan delay);
        void AddTask(Action action);
    }
}