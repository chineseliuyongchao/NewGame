using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:a8d70499-65c7-464f-8ac4-55c99feded75
	public partial class UIGameLobby
	{
		public const string Name = "UIGameLobby";
		
		[SerializeField]
		public UnityEngine.UI.Text timeView;
		
		private UIGameLobbyData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			timeView = null;
			
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
