using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.Game
{
    public class GameMenuController : BaseGameController
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.GetSystem<IGameSystem>().ChangeMainGameScene();
            }
        }
    }
}