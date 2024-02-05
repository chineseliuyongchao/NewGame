using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:ab4c8ee0-4e3b-47c4-b1ba-4b8a93670493
	public partial class UITownShowRole
	{
		public const string Name = "UITownShowRole";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI roleName;
		[SerializeField]
		private TMPro.TextMeshProUGUI countryName;
		[SerializeField]
		private TMPro.TextMeshProUGUI familyName;
		[SerializeField]
		private UnityEngine.UI.Button dialogue;
		
		private UITownShowRoleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			roleName = null;
			countryName = null;
			familyName = null;
			dialogue = null;
			
			mData = null;
		}
		
		public UITownShowRoleData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownShowRoleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownShowRoleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
