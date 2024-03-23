using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:188ddca3-9319-489a-8187-3f1f5935d412
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
		private TMPro.TextMeshProUGUI wealthValue;
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
			wealthValue = null;
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
