using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:7843a149-e039-4903-b9e8-2f212bf1e2c0
	public partial class UITownShowRole
	{
		public const string Name = "UITownShowRole";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI roleName;
		
		private UITownShowRoleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			roleName = null;
			
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
