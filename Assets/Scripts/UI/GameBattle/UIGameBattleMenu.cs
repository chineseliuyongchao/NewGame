using Game.GameBase;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIGameLobbyMenuData : UIPanelData
    {
    }

    /// <summary>
    /// 主界面菜单
    /// </summary>
    public partial class UIGameBattleMenu : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameLobbyMenuData ?? new UIGameLobbyMenuData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameLobbyMenuData ?? new UIGameLobbyMenuData();
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
            backToMenuButton.onClick.AddListener(() =>
            {
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE);
            });
            saveButton.onClick.AddListener(() =>
            {
                UIKit.OpenPanel<UIStartGamePanel>(new UIStartGamePanelData(false, false));
            });
            loadButton.onClick.AddListener(() =>
            {
                UIKit.OpenPanel<UIStartGamePanel>(new UIStartGamePanelData(true, false));
            });
            settingButton.onClick.AddListener(() => { UIKit.OpenPanel<UIGameSetting>(new UIGameSettingData()); });
            closeButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeBattleSceneEvent>(e =>
                {
                    if (!e.isChangeIn)
                    {
                        CloseSelf();
                    }
                })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override Transform AnimTransform()
        {
            return root;
        }
    }
}