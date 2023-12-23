using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:a3db9aab-72a9-4f2a-baec-21dba11e477a
	public partial class UIStartGamePanel
	{
		public const string Name = "UIStartGamePanel";
		
		[SerializeField]
		private UnityEngine.UI.Button newGameButton;
		[SerializeField]
		private UnityEngine.UI.Button newFileButton;
		[SerializeField]
		private RectTransform fileDataContent;
		[SerializeField]
		private UnityEngine.UI.Button backToMenu;
		[SerializeField]
		private UnityEngine.UI.Button backToGame;
		
		private UIStartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			newGameButton = null;
			newFileButton = null;
			fileDataContent = null;
			backToMenu = null;
			backToGame = null;
			
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
