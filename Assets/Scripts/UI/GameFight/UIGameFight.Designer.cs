using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:4732da4f-3309-4908-9a7d-fc37e50d4adf
	public partial class UIGameFight
	{
		public const string Name = "UIGameFight";
		
		[SerializeField]
		private UnityEngine.UI.Button exitButton;
		[SerializeField]
		private UnityEngine.UI.Button startButton;
		[SerializeField]
		private UnityEngine.UI.Button endRoundButton;
		
		private UIGameFightData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			exitButton = null;
			startButton = null;
			endRoundButton = null;
			
			mData = null;
		}
		
		public UIGameFightData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameFightData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameFightData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
