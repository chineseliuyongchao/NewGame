using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:7c86cac3-36de-4e7b-bd48-515aa2e1b755
	public partial class UIGameMenu
	{
		public const string Name = "UIGameMenu";
		
		
		private UIGameMenuData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UIGameMenuData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameMenuData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameMenuData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
