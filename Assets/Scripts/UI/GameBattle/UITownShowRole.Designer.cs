using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:535de898-7047-4560-8949-53798622439b
	public partial class UITownShowRole
	{
		public const string Name = "UITownShowRole";
		
		[SerializeField]
		private UnityEngine.UI.Text roleName;
		[SerializeField]
		private UnityEngine.UI.Text countryName;
		[SerializeField]
		private UnityEngine.UI.Text familyName;
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
