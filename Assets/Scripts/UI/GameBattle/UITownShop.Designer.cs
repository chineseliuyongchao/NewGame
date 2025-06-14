using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:cda21928-330e-4f01-b14b-a62fcea56e01
	public partial class UITownShop
	{
		public const string Name = "UITownShop";
		
		[SerializeField]
		private UnityEngine.UI.Button ButtonClose;
		
		private UITownShopData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ButtonClose = null;
			
			mData = null;
		}
		
		public UITownShopData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownShopData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownShopData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
