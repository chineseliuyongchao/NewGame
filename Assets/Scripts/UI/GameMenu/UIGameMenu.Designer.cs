using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:0e6a6813-bb4e-4f53-87ef-93733e8a1cc7
	public partial class UIGameMenu
	{
		public const string Name = "UIGameMenu";
		
		[SerializeField]
		public UnityEngine.UI.Button startGameButton;
		
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
