using System;
using System.Collections.Generic;

namespace Code.Core.Locator
{
    public sealed class ImplementationLocator : IDisposable
    {
        private readonly Dictionary<Type, IDisposable> Containers = new();
        private bool _isLocked;

        public ImplementationLocator(bool isLockAfterStart = true)
        {
            if (isLockAfterStart)
            {
                var locker = LocatorLocker.Create();
                locker.Complete += () => _isLocked = true;
            }
        }

        public void Add<T>(T implementation = default)
            where T : class, IUseLocator
        {
            if (_isLocked) return;

            if(Containers.TryGetValue(typeof(T), out var container))
            {
                container.Dispose();
            }

            Containers[typeof(T)] = new Container<T>(implementation);
        }

        public T Get<T>()
            where T : class, IUseLocator
        {
            try
            {
                var continer = (Container<T>)Containers[typeof(T)];
                return continer.Implementation;
            }
            catch
            {
                throw new NullReferenceException($"<color=red><b>[Locator Error]</b></color>: Не найден объект с типом {typeof(T)}");
            }
        }

        public void Forget<T>()
            where T : class, IUseLocator
        {
            if(Containers.TryGetValue(typeof(T),out var container))
            {
                container.Dispose();
            }

            Containers.Remove(typeof(T));
        }

        public void Dispose()
        {
            foreach (var container in Containers.Values)
            {
                container.Dispose();
            }

            Containers.Clear();

            GC.SuppressFinalize(this);
        }

        private sealed class Container<T> : IDisposable
            where T : class, IUseLocator
        {
            private T _implementation;
            private bool _isDisposed;

            public Container(T implemnetation = default)
            {
                _implementation = implemnetation;

                _isDisposed = typeof(T).GetInterface(nameof(IDisposable)) != null;
            }

            public T Implementation
            {
                get
                {
                    if(_implementation == null)
                    {
                        _implementation = Activator.CreateInstance<T>();
                    }

                    return _implementation;
                }
            }

            public void Dispose()
            {
                if (_isDisposed)
                {
                    var disposedImplementation = _implementation as IDisposable;
                    disposedImplementation.Dispose();
                }

                _implementation = null;

                GC.SuppressFinalize(this);
            }
        }
    }
}
