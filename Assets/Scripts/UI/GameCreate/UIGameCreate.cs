using GameQFramework;
using QFramework;

namespace UI
{
    public class UIGameCreateData : UIPanelData
    {
    }

    /// <summary>
    /// 创建游戏界面
    /// </summary>
    public partial class UIGameCreate : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameCreateData ?? new UIGameCreateData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameCreateData ?? new UIGameCreateData();
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
            createButton.onClick.AddListener(() =>
            {
                this.GetModel<IMyPlayerModel>().CreateGameData = new CreateGameData
                {
                    name = inputName.text.Equals("") ? "玩家" : inputName.text,
                    age = inputAge.text.ToInt(20)
                };
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeMainGameScene();
            });
            leaveButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeMenuScene();
            });
        }

        protected override void OnListenEvent()
        {
        }
    }
}