using System;
using System.Collections.Generic;

namespace Code.Core.ObjectsPool
{
    public abstract class ComplexPool<K, T> : IDisposable
        where T : class, IPoolObject
    {
        private readonly Dictionary<K, Pool<T>> Pools = new();

        public bool IsContainOf(K key) => Pools.ContainsKey(key);

        public T Spawn(K key)
        {
            if(Pools.TryGetValue(key, out var pool))
            {
                return pool.Spawn();
            }
            else
            {
                throw new NullReferenceException($"<color=red><b>[Pool Error]</b></color> Не добавлен пул для объектов с типом - {typeof(T)}.");
            }
        }

        public void Despawn(K key, in T @object)
        {
            if(!Pools.TryGetValue(key, out var pool))
            {
                pool.Despawn(@object);
            }
            else
            {
                throw new NullReferenceException($"<color=red><b>[Pool Error]</b></color> Не добавлен пул для объектов с типом - {typeof(T)}.");
            }
        }

        protected abstract Pool<T> CreateNewPool(in T prototype);

        protected void BindObject(K key, in T prototype)
        {
            Pools[key] = CreateNewPool(prototype);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
