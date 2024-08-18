using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:da9ce7a7-ce35-435e-86e7-9e2a9077e1aa
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
		private TMPro.TextMeshProUGUI townName;
		[SerializeField]
		private TMPro.TextMeshProUGUI prosperityValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI populationValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI levelValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI militiaValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI introduce;
		
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
