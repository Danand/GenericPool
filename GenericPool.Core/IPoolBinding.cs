namespace GenericPool
{
    public interface IPoolBinding
    {
        bool CheckType<TObject>();
        int SelectID(object instance);
        object SelectInstance(object templateObject);
        void OnGetFromPool(object instance);
        void OnPutToPool(object instance);
    }
}