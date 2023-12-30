using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:f77612df-d21f-4e41-875f-e4643776e011
	public partial class UITownShowRole
	{
		public const string Name = "UITownShowRole";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI roleName;
		[SerializeField]
		private TMPro.TextMeshProUGUI countryName;
		[SerializeField]
		private TMPro.TextMeshProUGUI familyName;
		
		private UITownShowRoleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			roleName = null;
			countryName = null;
			familyName = null;
			
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
