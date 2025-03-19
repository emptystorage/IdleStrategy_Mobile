using System;
using Code.Core.Locator;

namespace Code.Core
{
    public static class World
    {
        private static ImplementationLocator _locator;

        public static ImplementationLocator Locator
        {
            get
            {
                if(_locator == null )
                {
                    throw new NullReferenceException($"<color=red><b>[World Error]</b></color> Не создан локатор объектов!");
                }

                return _locator;
            }

            set
            {
                if(_locator == null)
                {
                    _locator = value;
                }
            }
        }

        public static bool IsLived => _locator != null;
    }
}
