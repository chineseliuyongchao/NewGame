using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:a60c2624-2897-4237-8336-d8501cbef7d5
	public partial class UIFightEnd
	{
		public const string Name = "UIFightEnd";
		
		[SerializeField]
		private UnityEngine.UI.Button exitButton;
		
		private UIFightEndData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			exitButton = null;
			
			mData = null;
		}
		
		public UIFightEndData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightEndData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightEndData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
