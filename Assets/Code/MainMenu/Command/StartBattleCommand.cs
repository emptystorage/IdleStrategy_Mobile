using UnityEngine.SceneManagement;

namespace Code.MainMenu.Command
{
    public ref struct StartBattleCommand
    {
        public void Execute(BattleLevelData data)
        {
            SceneManager.LoadScene(data.SceneName);
        }
    }
}
