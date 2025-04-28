using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:ab972b77-b0cb-4ca9-a06e-321642b41022
	public partial class UIFightCreateLegion
	{
		public const string Name = "UIFightCreateLegion";
		
		[SerializeField]
		private UnityEngine.UI.Text legionName;
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
