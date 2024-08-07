using Game.GameBase;
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
            InitUI();
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
            timeControlButton.onClick.AddListener(() =>
            {
                this.SendCommand(new TimePassCommand(!this.GetModel<IGameModel>().TimeIsPass));
            });
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(_ => { UpdateTime(); }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ChangeMainGameSceneEvent>(e =>
                {
                    if (!e.IsChangeIn)
                    {
                        CloseSelf();
                    }
                })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<TimePassEvent>(e => { UpdateTimePass(); }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ShowDialogEvent>(e => { UpdateTimePass(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void InitUI()
        {
            UpdateTime();
            UpdateTimePass();
        }

        private void UpdateTime()
        {
            timeView.text = this.GetModel<IGameModel>().NowTime.ToString();
        }

        private void UpdateTimePass()
        {
            if (this.GetModel<IGameModel>().TimeIsPass)
            {
                timePass.gameObject.SetActive(true);
                timePause.gameObject.SetActive(false);
            }
            else
            {
                timePass.gameObject.SetActive(false);
                timePause.gameObject.SetActive(true);
            }
        }
    }
}