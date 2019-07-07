using System;

namespace GenericPool.Core
{
    public sealed class PoolGetBuilder<TObject>
    {
        private readonly Pool pool;
        private readonly TObject instance;

        public PoolGetBuilder(Pool pool, TObject instance)
        {
            this.pool = pool;
            this.instance = instance;
        }

        public static implicit operator TObject(PoolGetBuilder<TObject> builder)
        {
            return builder.instance;
        }

        public PoolGetBuilder<TObject> Then(Action<TObject> action)
        {
            action.Invoke(instance);
            return this;
        }

        public void ThenPut()
        {
            pool.Put(instance);
        }
    }
}