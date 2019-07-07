using System;

namespace GenericPool
{
    public sealed class PoolBinding<TObject> : IPoolBinding
    {
        private readonly Func<TObject, int> idSelector;
        private readonly Func<TObject, TObject> instanceSelector;
        private readonly Action<TObject> getFromPoolCallback;
        private readonly Action<TObject> putToPoolCallback;

        public PoolBinding(
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

        bool IPoolBinding.CheckType<TInstance>()
        {
            return typeof(TObject).IsAssignableFrom(typeof(TInstance));
        }

        int IPoolBinding.SelectID(object instance)
        {
            return idSelector.Invoke((TObject)instance);
        }

        object IPoolBinding.SelectInstance(object templateObject)
        {
            return instanceSelector.Invoke((TObject)templateObject);
        }

        void IPoolBinding.OnGetFromPool(object instance)
        {
            getFromPoolCallback.Invoke((TObject)instance);
        }

        void IPoolBinding.OnPutToPool(object instance)
        {
            putToPoolCallback.Invoke((TObject)instance);
        }
    }
}