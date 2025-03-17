using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Code.Core.Locator;

namespace Code.Core
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void Initialize()
        {
            SceneManager.LoadScene(nameof(Bootstrap));
            SceneManager.sceneLoaded += Instance;
        }

        private static void Instance(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == nameof(Bootstrap))
            {
                new GameObject().AddComponent<Bootstrap>();
                SceneManager.sceneLoaded -= Instance;
            }
        }

        private IEnumerator Start()
        {
            World.Locator = new ImplementationLocator();

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
