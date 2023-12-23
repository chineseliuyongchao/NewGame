using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:0c395f16-7ef9-4b30-b584-a420334ea969
	public partial class UIGameLobbyMenu
	{
		public const string Name = "UIGameLobbyMenu";
		
		[SerializeField]
		private UnityEngine.UI.Button backToMenuButton;
		[SerializeField]
		private UnityEngine.UI.Button saveButton;
		[SerializeField]
		private UnityEngine.UI.Button loadButton;
		[SerializeField]
		private UnityEngine.UI.Button closeButton;
		
		private UIGameLobbyMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			backToMenuButton = null;
			saveButton = null;
			loadButton = null;
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
