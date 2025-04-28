using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:055cb71c-6f78-4d6f-b5ba-2a48a45ed9a2
	public partial class UITown
	{
		public const string Name = "UITown";
		
		[SerializeField]
		private UnityEngine.UI.Button showRoleButton;
		[SerializeField]
		private UnityEngine.UI.Button conscriptionButton;
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
