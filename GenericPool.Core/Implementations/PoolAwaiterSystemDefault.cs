using System;
using System.Threading.Tasks;

using GenericPool.Interfaces;

namespace GenericPool.Implementations
{
    public sealed class PoolAwaiterSystemDefault : IPoolAwaiter
    {
        private TimeSpan delay = TimeSpan.Zero;

        bool IPoolAwaiter.HasDelay
        {
            get { return delay != TimeSpan.Zero; }
        }

        void IPoolAwaiter.AddDelay(TimeSpan delay)
        {
            this.delay += delay;
        }

        void IPoolAwaiter.AddTask(Action action)
        {
            Task.Run(async () =>
            {
                await Task.Delay(delay);
                action.Invoke();
            });
        }
    }
}