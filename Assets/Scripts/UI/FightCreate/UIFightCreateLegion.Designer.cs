using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:10826aec-f858-4673-85c0-3b6a2d7f21bf
	public partial class UIFightCreateLegion
	{
		public const string Name = "UIFightCreateBelligerent";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI belligerentName;
		[SerializeField]
		private UnityEngine.UI.Button chooseButton;
		[SerializeField]
		private UnityEngine.UI.Button delete;
		
		private UIFightCreateBelligerentData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			belligerentName = null;
			chooseButton = null;
			delete = null;
			
			mData = null;
		}
		
		public UIFightCreateBelligerentData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightCreateBelligerentData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightCreateBelligerentData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
