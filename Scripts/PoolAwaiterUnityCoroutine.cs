using System;
using System.Collections;

using GenericPool.Interfaces;

using UnityEngine;

namespace GenericPool.Unity
{
    public sealed class PoolAwaiterUnityCoroutine : IPoolAwaiter
    {
        private readonly PoolUnity pool;

        private float delaySeconds;

        public PoolAwaiterUnityCoroutine(PoolUnity pool)
        {
            this.pool = pool;
        }

        bool IPoolAwaiter.HasDelay
        {
            get { return delaySeconds > 0; }
        }

        void IPoolAwaiter.AddDelay(TimeSpan delay)
        {
            delaySeconds += Convert.ToSingle(delay.TotalSeconds);
        }

        void IPoolAwaiter.AddTask(Action action)
        {
            pool.StartCoroutine(Awaiting(action));
        }

        private IEnumerator Awaiting(Action action)
        {
            yield return new WaitForSeconds(delaySeconds);
            action.Invoke();
        }
    }
}