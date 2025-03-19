using System;

namespace Code.Core.Common
{
    public sealed class ReactValue<T> : IDisposable
    {
        private T _value;
        private IDisposable _disposableValue;

        public ReactValue(T value = default)
        {
            _value = default;
            _disposableValue = typeof(T).GetInterface(nameof(IDisposable)) != null ? value as IDisposable : null;
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> ValueChanged;

        public void Dispose()
        {
            if(_disposableValue != null)
            {
                _disposableValue.Dispose();
            }

            _value = default;
            _disposableValue = null;

            ValueChanged = null;

            GC.SuppressFinalize(this);
        }
    }
}
