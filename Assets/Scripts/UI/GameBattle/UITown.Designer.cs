using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:e31ca876-1734-4ce6-8f5a-d7a6b23de0e4
	public partial class UITown
	{
		public const string Name = "UITown";
		
		[SerializeField]
		private UnityEngine.UI.Button showRoleButton;
		[SerializeField]
		private UnityEngine.UI.Button conscriptionButton;
		[SerializeField]
		private UnityEngine.UI.Button buildingButton;
		[SerializeField]
		private UnityEngine.UI.Button shopButton;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private UnityEngine.UI.Text townName;
		[SerializeField]
		private UnityEngine.UI.Text prosperityValue;
		[SerializeField]
		private UnityEngine.UI.Text populationValue;
		[SerializeField]
		private UnityEngine.UI.Text levelValue;
		[SerializeField]
		private UnityEngine.UI.Text militiaValue;
		[SerializeField]
		private UnityEngine.UI.Text introduce;
		
		private UITownData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			showRoleButton = null;
			conscriptionButton = null;
			buildingButton = null;
			shopButton = null;
			leaveButton = null;
			townName = null;
			prosperityValue = null;
			populationValue = null;
			levelValue = null;
			militiaValue = null;
			introduce = null;
			
			mData = null;
		}
		
		public UITownData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
