using System;
using System.Collections.Generic;

namespace Code.Core.ObjectsPool
{
    public abstract class Pool<T> : IDisposable
        where T : class, IPoolObject
    {
        private readonly Stack<T> ObjectStack = new();
        private readonly T Prototype;

        public Pool(T prototype)
        {
            if (prototype == null)
                throw new ArgumentNullException($"<color=red><b>[Pool Error]</b></color> Не указан прототип (тип = {typeof(T)}) объекта для клонирования.");

            Prototype = prototype;
        }

        public T Spawn()
        {
            var @object = ObjectStack.Count > 0 ? ObjectStack.Pop() : CreateObject(Prototype);
            Spawned(@object);

            return @object;
        }

        public void Despawn(in T @object)
        {
            ObjectStack.Push(@object);
            Despawned(@object);
        }

        protected abstract T CreateObject(in T prototype);
        protected virtual void Spawned(in T @object) { }
        protected virtual void Despawned(in T @object) { }
        protected virtual void ObjectDisposed(T @object) { }

        public void Dispose()
        {
            while(ObjectStack.Count > 0)
            {
                var @object = ObjectStack.Pop();
                ObjectDisposed(@object);
            }

            GC.SuppressFinalize(this);
        }
    }
}
