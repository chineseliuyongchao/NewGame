using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:014a1702-be13-4aa9-90b4-be8a2bda8f09
	public partial class UIStartGamePanel
	{
		public const string Name = "UIStartGamePanel";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private UnityEngine.UI.Button newGameButton;
		[SerializeField]
		private UnityEngine.UI.Button newFileButton;
		[SerializeField]
		private RectTransform fileDataContent;
		[SerializeField]
		private UnityEngine.UI.Button backToMenuButton;
		[SerializeField]
		private UnityEngine.UI.Button backToGameButton;
		
		private UIStartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
			newGameButton = null;
			newFileButton = null;
			fileDataContent = null;
			backToMenuButton = null;
			backToGameButton = null;
			
			mData = null;
		}
		
		public UIStartGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIStartGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIStartGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
