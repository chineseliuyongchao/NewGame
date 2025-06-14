using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:217fe806-3944-47b2-b122-d763611f8208
	public partial class UITownBuilding
	{
		public const string Name = "UITownBuilding";
		
		[SerializeField]
		private UnityEngine.UI.Button ButtonClose;
		
		private UITownBuildingData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ButtonClose = null;
			
			mData = null;
		}
		
		public UITownBuildingData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownBuildingData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownBuildingData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
