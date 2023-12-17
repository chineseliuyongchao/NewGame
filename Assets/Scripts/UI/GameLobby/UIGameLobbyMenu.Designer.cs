using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:1e6c5c7b-4f3c-40fa-b80a-03ae6bc3c1d7
	public partial class UIGameLobbyMenu
	{
		public const string Name = "UIGameLobbyMenu";
		
		[SerializeField]
		public UnityEngine.UI.Button backToMenuButton;
		[SerializeField]
		public UnityEngine.UI.Button saveButton;
		[SerializeField]
		public UnityEngine.UI.Button closeButton;
		
		private UIGameLobbyMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			backToMenuButton = null;
			saveButton = null;
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
