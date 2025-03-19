using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Code.Core.Locator;
using Code.Battle;
using Code.Battle.Pool;

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
            var locator = new ImplementationLocator();
            locator.Add<BattleInformation>();
            locator.Add<BattleResourceContainer>(new());
            locator.Add<UnitPool>();

            World.Locator = locator;

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
