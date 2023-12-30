using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:8367cdca-abfb-4d8b-8f59-a36b2e0570be
	public partial class UITownRole
	{
		public const string Name = "UITownRole";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private RectTransform Content;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UITownRoleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
			Content = null;
			leaveButton = null;
			
			mData = null;
		}
		
		public UITownRoleData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownRoleData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownRoleData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
