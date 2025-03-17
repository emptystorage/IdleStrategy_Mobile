using UnityEngine;

namespace Code.MainMenu
{
    [CreateAssetMenu(fileName = "New"+nameof(BattleLevelData), menuName = "GameData/New"+nameof(BattleLevelData))]
    public sealed class BattleLevelData : ScriptableObject
    {
        private string _sceneName;

#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _scene;

        private void OnValidate()
        {
            if(_scene != null)
            {
                _sceneName = _scene.name;
            }
        }
#endif

        public string SceneName => _sceneName;
    }
}
