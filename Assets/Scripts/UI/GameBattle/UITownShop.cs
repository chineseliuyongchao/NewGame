using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	public class UITownShopData : UIPanelData
	{
	}
	public partial class UITownShop : UIBase
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UITownShopData ?? new UITownShopData();
			// please add init code here
			base.OnInit(uiData);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			mData = uiData as UITownShopData ?? new UITownShopData();
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
			ButtonClose.onClick.AddListener(OnButtonCloseClick);
		}
		
		protected override void OnListenEvent()
		{
		}
		
		#region

		private void OnButtonCloseClick()
		{
			CloseSelf();
		}

		#endregion
	}
}
