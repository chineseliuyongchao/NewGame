using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:483b5860-40d4-4ad3-b52b-3e93faddebb5
	public partial class UIGameLobbyMenu
	{
		public const string Name = "UIGameLobbyMenu";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private UnityEngine.UI.Button backToMenuButton;
		[SerializeField]
		private UnityEngine.UI.Button saveButton;
		[SerializeField]
		private UnityEngine.UI.Button loadButton;
		[SerializeField]
		private UnityEngine.UI.Button settingButton;
		[SerializeField]
		private UnityEngine.UI.Button closeButton;
		
		private UIGameLobbyMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
			backToMenuButton = null;
			saveButton = null;
			loadButton = null;
			settingButton = null;
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
