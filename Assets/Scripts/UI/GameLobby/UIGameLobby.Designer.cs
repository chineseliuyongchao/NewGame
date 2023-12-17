using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:f0f826f3-5d7d-4473-a1e8-82ab7dde412d
	public partial class UIGameLobby
	{
		public const string Name = "UIGameLobby";
		
		[SerializeField]
		public TMPro.TextMeshProUGUI timeView;
		[SerializeField]
		public UnityEngine.UI.Button menuButton;
		
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
