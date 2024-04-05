using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:4bad3f0b-915d-4d40-88eb-d74f88f6c2b3
	public partial class UIGameMenu
	{
		public const string Name = "UIGameMenu";
		
		[SerializeField]
		private UnityEngine.UI.Button startGameButton;
		[SerializeField]
		private UnityEngine.UI.Button settingButton;
		
		private UIGameMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			startGameButton = null;
			settingButton = null;
			
			mData = null;
		}
		
		public UIGameMenuData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameMenuData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameMenuData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
