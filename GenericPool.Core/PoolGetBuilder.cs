using System;

using GenericPool.Interfaces;

namespace GenericPool
{
    public sealed class PoolGetBuilder<TObject>
    {
        private readonly Pool pool;
        private readonly TObject instance;
        private readonly IPoolAwaiter poolAwaiter;

        public PoolGetBuilder(Pool pool, TObject instance, IPoolAwaiter poolAwaiter)
        {
            this.pool = pool;
            this.instance = instance;
            this.poolAwaiter = poolAwaiter;
        }

        public static implicit operator TObject(PoolGetBuilder<TObject> builder)
        {
            return builder.instance;
        }

        public PoolGetBuilder<TObject> Then(Action<TObject> action)
        {
            if (poolAwaiter.HasDelay)
                poolAwaiter.AddTask(() => action.Invoke(instance));
            else
                action.Invoke(instance);

            return this;
        }

        public PoolGetBuilder<TObject> ThenWait(Func<TObject, TimeSpan> delaySelector)
        {
            var delay = delaySelector.Invoke(instance);
            poolAwaiter.AddDelay(delay);
            return this;
        }

        public void ThenPut()
        {
            if (poolAwaiter.HasDelay)
                poolAwaiter.AddTask(() => pool.Put(instance));
            else
                pool.Put(instance);
        }
    }
}