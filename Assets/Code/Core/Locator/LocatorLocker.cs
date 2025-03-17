using System;
using System.Collections;
using UnityEngine;

namespace Code.Core.Locator
{
    public sealed class LocatorLocker : MonoBehaviour
    {
        public event Action Complete;

        public static LocatorLocker Create()
        {
            return new GameObject().AddComponent<LocatorLocker>();
        }

        private IEnumerator Start()
        {
            yield return null;

            Complete?.Invoke();
            Complete = null;

            Destroy(gameObject);
        }
    }
}
