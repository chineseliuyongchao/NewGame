using Game.GameMenu;
using QFramework;
using UnityEngine.Localization.Settings;

namespace UI
{
    public class UIGameSettingData : UIPanelData
    {
    }

    /// <summary>
    /// 设置界面
    /// </summary>
    public partial class UIGameSetting : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameSettingData ?? new UIGameSettingData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameSettingData ?? new UIGameSettingData();
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
            Dropdown.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameMenuModel>().Language = value;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
            });
            leaveButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }
    }
}