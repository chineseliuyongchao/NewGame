using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:1e3193b8-2de8-4c2d-a034-b5bd6d4cc3e1
	public partial class UIGameMenu
	{
		public const string Name = "UIGameMenu";
		
		[SerializeField]
		private UnityEngine.UI.Button startGameButton;
		[SerializeField]
		private UnityEngine.UI.Button fightButton;
		[SerializeField]
		private UnityEngine.UI.Button settingButton;
		
		private UIGameMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			startGameButton = null;
			fightButton = null;
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
