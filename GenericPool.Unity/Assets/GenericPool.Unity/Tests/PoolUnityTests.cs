using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

namespace GenericPool.Unity.Tests
{
    public class PoolUnityTests
    {
        [UnityTest]
        public IEnumerator Get_PropertiesAreEqual()
        {
            var pool = new GameObject(nameof(PoolUnity)).AddComponent<PoolUnity>();
            yield return null;

            var prefab = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;

            prefab.name = "Cube";
            prefab.localScale = Vector3.one * 2;

            yield return null;

            var instance = pool.Get(prefab);

            yield return null;

            Assert.AreEqual(prefab.localScale, instance.localScale);
        }

        [UnityTest]
        public IEnumerator Put_OneObjectManyTimes()
        {
            var pool = new GameObject(nameof(PoolUnity)).AddComponent<PoolUnity>();
            yield return null;

            var prefab = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            prefab.name = "Cube";

            yield return null;

            var objectAmount = 10;

            for (int i = 0; i < objectAmount; i++)
            {
                var instance = pool.Get(prefab);
                yield return null;

                pool.Put(instance);
                yield return null;
            }

            yield return null;

            Assert.AreEqual(1, pool.transform.childCount);
        }

        [UnityTest]
        public IEnumerator Put_ManyObjectsManyTimes()
        {
            var pool = new GameObject(nameof(PoolUnity)).AddComponent<PoolUnity>();
            yield return null;

            var prefab = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            prefab.name = "Cube";

            yield return null;

            var objectAmount = 10;
            var instances = new List<Transform>();

            for (int i = 0; i < objectAmount; i++)
            {
                var instance = pool.Get(prefab);
                instances.Add(instance);
                yield return null;
            }

            foreach (Transform instance in instances)
            {
                pool.Put(instance);
                yield return null;
            }

            yield return null;

            Assert.AreEqual(objectAmount, pool.transform.childCount);
        }
    }
}
