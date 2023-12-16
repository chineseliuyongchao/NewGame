using QFramework;
using UnityEngine.SceneManagement;

namespace GameQFramework
{
    public class GameSystem: AbstractSystem, IGameSystem
    {
        protected override void OnInit()
        {
            
        }

        public void ChangeMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void ChangeMainGameScene()
        {
            SceneManager.LoadScene("MainGameScene");
        }
    }
}