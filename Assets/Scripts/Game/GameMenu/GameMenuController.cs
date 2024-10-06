using Game.GameBase;
using Game.GameMenu;
using QFramework;
using UI;
using UnityEngine.Localization.Settings;

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
            this.GetModel<IGameMenuModel>().RevertMenuTime++;
            if (this.GetModel<IGameMenuModel>().RevertMenuTime <= 1)
            {
                ResKit.Init(); //ResKit初始化以后才能使用ResKit和UIKit相关内容
                this.GetSystem<IGameSystem>().LoadCurrentData();
            }

            LocalizationSettings.SelectedLocale =
                LocalizationSettings.AvailableLocales.Locales[this.GetModel<IGameSettingModel>().Language];
        }

        protected override void OnControllerStart()
        {
            UIKit.OpenPanel<UIGameMenu>();
        }
    }
}