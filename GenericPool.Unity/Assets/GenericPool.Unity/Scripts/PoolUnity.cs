using UnityEngine;

using GenericPool.Core;

namespace GenericPool.Unity
{
    public sealed class PoolUnity : MonoBehaviour
    {
        private Pool pool;

        void Awake()
        {
            pool = new Pool();

            pool.Register<Component>(
                idSelector:             instance => instance.name.Replace("(Clone)", string.Empty).GetHashCode(),
                instanceSelector:       Instantiate,
                getFromPoolCallback:    instance =>
                                        {
                                            instance.transform.SetParent(null, true);
                                            instance.gameObject.SetActive(true);

                                        },
                putToPoolCallback:      instance =>
                                        {
                                            instance.transform.SetParent(transform, false);
                                            instance.gameObject.SetActive(false);
                                        });
        }

        public TComponent Get<TComponent>(TComponent prefab)
            where TComponent : Component
        {
            return pool.Get(prefab);
        }

        public PoolGetBuilder<TComponent> GetWith<TComponent>(TComponent prefab)
            where TComponent : Component
        {
            return pool.GetWith(prefab);
        }

        public void Put<TComponent>(TComponent instance)
            where TComponent : Component
        {
            pool.Put(instance);
        }
    }
}
