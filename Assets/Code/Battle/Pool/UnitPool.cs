using UnityEngine;
using Code.Battle.Units;
using Code.Core.Locator;
using Code.Core.ObjectsPool;

namespace Code.Battle.Pool
{
    public sealed class UnitPool : ComplexPool<string, Unit>, IUseLocator
    {
        private readonly Transform Root;

        public UnitPool()
        {
            Root = new GameObject(nameof(UnitPool)).transform;
        }

        public Unit Spawn(in Unit prefab, Vector3 position)
        {
            if (!IsContainOf(prefab.name))
            {
                BindObject(prefab.name, prefab);
            }

            var unit = base.Spawn(prefab.name);
            unit.transform.position = position;

            return unit;
        }

        public void Despawn(in Unit @object)
        {
            base.Despawn(@object.name, @object);
        }

        protected override Pool<Unit> CreateNewPool(in Unit prototype)
        {
            return new MyPool(prototype, Root);
        }

        private sealed class MyPool : Pool<Unit>
        {
            private readonly Transform Root;

            public MyPool(Unit prototype, Transform root) : base(prototype) 
            { 
                Root = root;
            }

            protected override Unit CreateObject(in Unit prototype)
            {
                var unit = MonoBehaviour.Instantiate(prototype);
                unit.name = prototype.name;
                unit.transform.SetParent(Root);

                return unit;
            }

            protected override void Spawned(in Unit @object)
            {
                @object.gameObject.SetActive(true);
            }

            protected override void Despawned(in Unit @object)
            {
                @object.gameObject.SetActive(false);
            }

            protected override void ObjectDisposed(Unit @object)
            {
                MonoBehaviour.Destroy(@object);
            }
        }
    }
}
