using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	public class UITownBuildingData : UIPanelData
	{
	}
	public partial class UITownBuilding : UIBase
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UITownBuildingData ?? new UITownBuildingData();
			// please add init code here
			base.OnInit(uiData);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			mData = uiData as UITownBuildingData ?? new UITownBuildingData();
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
