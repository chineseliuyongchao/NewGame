using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:de36eff8-87e3-440a-84af-adba679fa029
	public partial class UIGameFight
	{
		public const string Name = "UIGameFight";
		
		[SerializeField]
		private UnityEngine.UI.Button exitButton;
		[SerializeField]
		private UnityEngine.UI.Button startButton;
		[SerializeField]
		private UnityEngine.UI.Button endRoundButton;
		[SerializeField]
		private RectTransform fightBehaviorGroup;
		[SerializeField]
		private UnityEngine.UI.Button advanceButton;
		[SerializeField]
		private UnityEngine.UI.Button shootButton;
		[SerializeField]
		private UnityEngine.UI.Button sustainedAdvanceButton;
		[SerializeField]
		private UnityEngine.UI.Button sustainedShootButton;
		[SerializeField]
		private UnityEngine.UI.Button chargeButton;
		[SerializeField]
		private UnityEngine.UI.Button holdButton;
		[SerializeField]
		private UnityEngine.UI.Button turnButton;
		[SerializeField]
		private UnityEngine.UI.Button retreatButton;
		
		private UIGameFightData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			exitButton = null;
			startButton = null;
			endRoundButton = null;
			fightBehaviorGroup = null;
			advanceButton = null;
			shootButton = null;
			sustainedAdvanceButton = null;
			sustainedShootButton = null;
			chargeButton = null;
			holdButton = null;
			turnButton = null;
			retreatButton = null;
			
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
