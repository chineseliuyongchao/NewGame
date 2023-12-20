using QFramework;
using UnityEngine.SceneManagement;

namespace GameQFramework
{
    public class GameSystem : AbstractSystem, IGameSystem
    {
        protected override void OnInit()
        {
        }

        public void ChangeMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void ChangeMainGameScene(string fileName = null)
        {
            this.SendEvent(new ChangeToMainGameSceneEvent());
            this.GetSystem<IGameSaveSystem>().LoadGame(fileName);
            SceneManager.LoadScene("MainGameScene");
        }
    }
}