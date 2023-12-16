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
        }

        protected override void OnListenEvent()
        {
        }
    }
}