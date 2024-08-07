using Game.GameBase;
using QFramework;

namespace UI
{
    public class UIFightCreateData : UIPanelData
    {
    }

    /// <summary>
    /// 创建战斗的界面
    /// </summary>
    public partial class UIFightCreate : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateData ?? new UIFightCreateData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateData ?? new UIFightCreateData();
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
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.FIGHT_SCENE);
            });
            leaveButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE);
            });
        }

        protected override void OnListenEvent()
        {
        }
    }
}