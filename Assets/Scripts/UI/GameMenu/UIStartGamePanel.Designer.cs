using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:150270af-4d02-44d5-9c3e-67ac76a2d9a7
	public partial class UIStartGamePanel
	{
		public const string Name = "UIStartGamePanel";
		
		[SerializeField]
		private UnityEngine.UI.Button newGameButton;
		[SerializeField]
		private RectTransform fileDataContent;
		[SerializeField]
		private UnityEngine.UI.Button backToMenu;
		
		private UIStartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			newGameButton = null;
			fileDataContent = null;
			backToMenu = null;
			
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
