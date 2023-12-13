using GameQFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Game
{
    public class GamePassController : BaseGameController
    {
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}