using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:9f4a102c-acbe-4a10-8c45-7935c9d8f78e
	public partial class UIFightCreate
	{
		public const string Name = "UIFightCreate";
		
		[SerializeField]
		private UnityEngine.UI.Button createButton;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UIFightCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			
			mData = null;
		}
		
		public UIFightCreateData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightCreateData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightCreateData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
