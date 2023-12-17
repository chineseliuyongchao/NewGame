using GameQFramework;
using QFramework;

namespace UI
{
    public class UIGameLobbyData : UIPanelData
    {
    }

    /// <summary>
    /// 游戏大厅界面
    /// </summary>
    public partial class UIGameLobby : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameLobbyData ?? new UIGameLobbyData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameLobbyData ?? new UIGameLobbyData();
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
            menuButton.onClick.AddListener(() => { UIKit.OpenPanel<UIGameLobbyMenu>(); });
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(e =>
            {
                timeView.text = this.GetModel<IGameModel>().Year + "年" + this.GetModel<IGameModel>().Month + "月" +
                                this.GetModel<IGameModel>().Day + "日" + this.GetModel<IGameModel>().Time + "时";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ChangeToMenuSceneEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}