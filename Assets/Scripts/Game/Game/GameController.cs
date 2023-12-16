using GameQFramework;
using QFramework;
using UI;

namespace Game.Game
{
    public class GameController : BaseGameController
    {
        protected override void OnControllerStart()
        {
            base.OnControllerStart();
            UIKit.OpenPanel<UIGameLobby>();
        }
    }
}