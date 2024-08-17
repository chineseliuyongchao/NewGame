using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:303066b8-a4a2-4e70-91d5-288a71388f6f
	public partial class UIGameFight
	{
		public const string Name = "UIGameFight";
		
		[SerializeField]
		private UnityEngine.UI.Button exitButton;
		
		private UIGameFightData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			exitButton = null;
			
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
