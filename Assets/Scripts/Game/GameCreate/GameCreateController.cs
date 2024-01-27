using GameQFramework;
using QFramework;
using UI;

namespace Game
{
    /// <summary>
    /// 新建游戏场景控制
    /// </summary>
    public class GameCreateController : BaseGameController
    {
        protected override void OnControllerStart()
        {
            UIKit.OpenPanel<UIGameCreate>();
        }
    }
}