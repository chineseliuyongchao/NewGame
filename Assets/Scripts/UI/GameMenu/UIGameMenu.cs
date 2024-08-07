using Game.GameBase;
using QFramework;

namespace UI
{
    public class UIGameMenuData : UIPanelData
    {
    }

    /// <summary>
    /// 菜单界面的UI
    /// </summary>
    public partial class UIGameMenu : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameMenuData ?? new UIGameMenuData();
            // please add init code here
            base.OnInit(uiData);
            OnInitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameMenuData ?? new UIGameMenuData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        protected override void OnListenButton()
        {
            startGameButton.onClick.AddListener(
                () => { UIKit.OpenPanel<UIStartGamePanel>(new UIStartGamePanelData()); });
            fightButton.onClick.AddListener(() =>
            {
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.CREATE_FIGHT_SCENE);
            });
            settingButton.onClick.AddListener(() => { UIKit.OpenPanel<UIGameSetting>(new UIGameSettingData()); });
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeMenuSceneEvent>(e =>
                {
                    if (!e.IsChangeIn)
                    {
                        CloseSelf();
                    }
                })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnInitUI()
        {
        }
    }
}