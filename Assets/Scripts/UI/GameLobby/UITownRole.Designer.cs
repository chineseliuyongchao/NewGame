using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:f1b783d4-cfc7-4fdf-833d-67cd4a70cd7a
	public partial class UITownRole
	{
		public const string Name = "UITownRole";
		
		[SerializeField]
		private RectTransform Content;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UITownRoleData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
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
