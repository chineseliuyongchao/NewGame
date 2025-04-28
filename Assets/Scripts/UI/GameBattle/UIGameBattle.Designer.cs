using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:ac4682d2-8b65-4470-b880-ae2117e3c445
	public partial class UIGameBattle
	{
		public const string Name = "UIGameBattle";
		
		[SerializeField]
		private UnityEngine.UI.Text timeView;
		[SerializeField]
		private UnityEngine.UI.Button menuButton;
		[SerializeField]
		private UnityEngine.UI.Button timeControlButton;
		[SerializeField]
		private UnityEngine.UI.Image timePause;
		[SerializeField]
		private UnityEngine.UI.Image timePass;
		
		private UIGameBattleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			timeView = null;
			menuButton = null;
			timeControlButton = null;
			timePause = null;
			timePass = null;
			
			mData = null;
		}
		
		public UIGameBattleData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameBattleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameBattleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
