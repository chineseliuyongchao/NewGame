using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:c2397838-9c60-43f2-b122-437f9dc11b61
	public partial class UIGameLobby
	{
		public const string Name = "UIGameLobby";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI timeView;
		[SerializeField]
		private UnityEngine.UI.Button menuButton;
		
		private UIGameLobbyData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			timeView = null;
			menuButton = null;
			
			mData = null;
		}
		
		public UIGameLobbyData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameLobbyData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameLobbyData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
