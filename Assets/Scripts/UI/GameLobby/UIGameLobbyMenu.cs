using GameQFramework;
using QFramework;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIGameLobbyMenuData : UIPanelData
    {
    }

    /// <summary>
    /// 主界面菜单
    /// </summary>
    public partial class UIGameLobbyMenu : UIBase
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
                this.SendCommand(new ChangeToMenuSceneCommand());
                this.GetSystem<IGameSystem>().ChangeMenuScene();
            });
            saveButton.onClick.AddListener(() =>
            {
                this.GetSystem<IGameSaveSystem>().SaveGame(001.ToString()); //测试使用
            });
            closeButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeToMenuSceneEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}