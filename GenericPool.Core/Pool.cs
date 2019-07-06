using System;
using System.Collections.Generic;

namespace GenericPool.Core
{
    public sealed class Pool<TObject>
    {
        private readonly Dictionary<int, Queue<TObject>> pools = new Dictionary<int, Queue<TObject>>();

        private readonly Func<TObject, int> idSelector;
        private readonly Func<TObject, TObject> instanceSelector;
        private readonly Action<TObject> getFromPoolCallback;
        private readonly Action<TObject> putToPoolCallback;

        public Pool(
            Func<TObject, int>      idSelector,
            Func<TObject, TObject>  instanceSelector,
            Action<TObject>         getFromPoolCallback,
            Action<TObject>         putToPoolCallback)
        {
            this.idSelector = idSelector;
            this.instanceSelector = instanceSelector;
            this.getFromPoolCallback = getFromPoolCallback;
            this.putToPoolCallback = putToPoolCallback;
        }

        public TObject Get(TObject templateObject)
        {
            var id = idSelector.Invoke(templateObject);

            TObject instance;

            if (pools.TryGetValue(id, out var pool))
            {
                if (pool == null)
                    pool = pools[id] = new Queue<TObject>();

                if (pool.Peek() == null)
                {
                    instance = instanceSelector(templateObject);
                }
                else
                {
                    instance = pool.Dequeue();
                    getFromPoolCallback.Invoke(instance);
                }
            }
            else
            {
                instance = instanceSelector(templateObject);
            }

            return instance;
        }

        public void Put(TObject instance)
        {
            var id = idSelector.Invoke(instance);

            if (!pools.TryGetValue(id, out var pool) || pool == null)
                pool = pools[id] = new Queue<TObject>();

            putToPoolCallback.Invoke(instance);

            pool.Enqueue(instance);
        }
    }
}
