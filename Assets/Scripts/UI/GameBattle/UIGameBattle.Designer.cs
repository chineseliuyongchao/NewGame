using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:83fd43f0-52ba-47b9-88ee-a5b140e7f4b2
	public partial class UIGameBattle
	{
		public const string Name = "UIGameLobby";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI timeView;
		[SerializeField]
		private UnityEngine.UI.Button menuButton;
		[SerializeField]
		private UnityEngine.UI.Button timeControlButton;
		[SerializeField]
		private UnityEngine.UI.Image timePause;
		[SerializeField]
		private UnityEngine.UI.Image timePass;
		
		private UIGameLobbyData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			timeView = null;
			menuButton = null;
			timeControlButton = null;
			timePause = null;
			timePass = null;
			
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
