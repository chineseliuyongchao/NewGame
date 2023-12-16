using GameQFramework;
using QFramework;

namespace UI
{
    public class UIStartGamePanelData : UIPanelData
    {
    }

    /// <summary>
    /// 开始游戏界面
    /// </summary>
    public partial class UIStartGamePanel : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIStartGamePanelData ?? new UIStartGamePanelData();
            // please add init code here
            base.OnInit(uiData);
            OnInitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIStartGamePanelData ?? new UIStartGamePanelData();
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
            newGameButton.onClick.AddListener(() =>
            {
                this.SendCommand(new ChangeToMainGameSceneCommand());
                this.GetSystem<IGameSystem>().ChangeMainGameScene();
            });
            backToMenu.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeToMainGameSceneEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnInitUI()
        {
        }
    }
}