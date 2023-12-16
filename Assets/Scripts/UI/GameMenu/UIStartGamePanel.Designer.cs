using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:d3aa9113-9667-4890-846a-57a9f0c6745a
	public partial class UIStartGamePanel
	{
		public const string Name = "UIStartGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button newGameButton;
		[SerializeField]
		public UnityEngine.UI.Button backToMenu;
		
		private UIStartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			newGameButton = null;
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
