using Game.GameBase;
using QFramework;
using UI;

namespace Game
{
    /// <summary>
    /// 新建游戏场景控制
    /// </summary>
    public class FightCreateController : BaseGameController
    {
        protected override void OnControllerStart()
        {
            UIKit.OpenPanel<UIFightCreate>();
        }
    }
}