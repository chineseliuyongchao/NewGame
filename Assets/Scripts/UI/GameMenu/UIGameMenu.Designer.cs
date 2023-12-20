using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:8c014a6c-24ac-4dfa-b966-3af2dccfb27b
	public partial class UIGameMenu
	{
		public const string Name = "UIGameMenu";
		
		[SerializeField]
		private UnityEngine.UI.Button startGameButton;
		
		private UIGameMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			startGameButton = null;
			
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
