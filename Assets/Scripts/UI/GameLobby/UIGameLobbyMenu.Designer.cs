using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:d63ee5bd-bfdf-4115-9cfe-be652ac65109
	public partial class UIGameLobbyMenu
	{
		public const string Name = "UIGameLobbyMenu";
		
		[SerializeField]
		public UnityEngine.UI.Button backToMenuButton;
		[SerializeField]
		public UnityEngine.UI.Button closeButton;
		
		private UIGameLobbyMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			backToMenuButton = null;
			closeButton = null;
			
			mData = null;
		}
		
		public UIGameLobbyMenuData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameLobbyMenuData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameLobbyMenuData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
