using Game.GameBase;
using QFramework;
using UI;

namespace Game
{
    /// <summary>
    /// 新建战役场景控制
    /// </summary>
    public class BattleCreateController : BaseGameController
    {
        protected override void OnControllerStart()
        {
            UIKit.OpenPanel<UIBattleCreate>();
        }
    }
}