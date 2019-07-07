using System;
using System.Collections.Generic;
using System.Linq;

using GenericPool.Implementations;
using GenericPool.Interfaces;

namespace GenericPool
{
    public sealed class Pool
    {
        private readonly Dictionary<int, Queue<object>> instances = new Dictionary<int, Queue<object>>();
        private readonly List<IPoolBinding> bindings = new List<IPoolBinding>();

        public IPoolAwaiter PoolAwaiter { get; set; } = new PoolAwaiterSystemDefault();

        public void Register<TObject>(
            Func<TObject, int>      idSelector,
            Func<TObject, TObject>  instanceSelector,
            Action<TObject>         getFromPoolCallback,
            Action<TObject>         putToPoolCallback)
        {
            IPoolBinding binding = new PoolBinding<TObject>(idSelector, instanceSelector, getFromPoolCallback, putToPoolCallback);
            bindings.Add(binding);
        }

        public TObject Get<TObject>(TObject templateObject)
        {
            return GetWith(templateObject);
        }

        public PoolGetBuilder<TObject> GetWith<TObject>(TObject templateObject)
        {
            var foundBinding = bindings.First(binding => binding.CheckType<TObject>());
            var id = foundBinding.SelectID(templateObject);

            TObject instance;

            if (instances.TryGetValue(id, out var queue))
            {
                if (queue == null)
                    queue = instances[id] = new Queue<object>();

                if (queue.Peek() == null)
                {
                    instance = (TObject)foundBinding.SelectInstance(templateObject);
                }
                else
                {
                    instance = (TObject)queue.Dequeue();
                    foundBinding.OnGetFromPool(instance);
                }
            }
            else
            {
                instance = (TObject)foundBinding.SelectInstance(templateObject);
            }

            return new PoolGetBuilder<TObject>(this, instance, PoolAwaiter);
        }

        public void Put<TObject>(TObject instance)
        {
            var foundBinding = bindings.First(binding => binding.CheckType<TObject>());
            var id = foundBinding.SelectID(instance);

            if (!instances.TryGetValue(id, out var pool) || pool == null)
                pool = instances[id] = new Queue<object>();

            foundBinding.OnPutToPool(instance);

            pool.Enqueue(instance);
        }
    }
}
