using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:7bd1507d-28a8-418e-a100-81d181a2d78f
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
		private UnityEngine.UI.Button closeButton;
		
		private UIGameLobbyMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
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
