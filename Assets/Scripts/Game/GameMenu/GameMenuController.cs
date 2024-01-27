using GameQFramework;
using QFramework;
using UI;

namespace Game
{
    /// <summary>
    /// 游戏主菜单场景控制
    /// </summary>
    public class GameMenuController : BaseGameController
    {
        protected override void OnInit()
        {
            base.OnInit();
            ResKit.Init(); //ResKit初始化以后才能使用ResKit和UIKit相关内容
        }

        protected override void OnControllerStart()
        {
            UIKit.OpenPanel<UIGameMenu>();
        }
    }
}