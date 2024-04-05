using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:688dea7d-52fa-492a-af3b-829f882faa99
	public partial class UIGameSetting
	{
		public const string Name = "UIGameSetting";
		
		[SerializeField]
		private TMPro.TMP_Dropdown Dropdown;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UIGameSettingData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Dropdown = null;
			leaveButton = null;
			
			mData = null;
		}
		
		public UIGameSettingData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameSettingData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameSettingData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
