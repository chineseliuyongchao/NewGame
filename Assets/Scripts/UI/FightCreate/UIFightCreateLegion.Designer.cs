using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:1718a8ab-2ac1-455f-8526-fbb8caaa210f
	public partial class UIFightCreateLegion
	{
		public const string Name = "UIFightCreateLegion";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI legionName;
		[SerializeField]
		private UnityEngine.UI.Button chooseButton;
		[SerializeField]
		private UnityEngine.UI.Button delete;
		
		private UIFightCreateLegionData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			legionName = null;
			chooseButton = null;
			delete = null;
			
			mData = null;
		}
		
		public UIFightCreateLegionData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightCreateLegionData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightCreateLegionData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
