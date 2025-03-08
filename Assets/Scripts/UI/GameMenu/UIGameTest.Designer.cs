using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:58d834c6-747b-448d-bc65-6747b2603689
	public partial class UIGameTest
	{
		public const string Name = "UIGameTest";
		
		[SerializeField]
		private UnityEngine.UI.Button testButton;
		
		private UIGameTestData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			testButton = null;
			
			mData = null;
		}
		
		public UIGameTestData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameTestData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameTestData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
