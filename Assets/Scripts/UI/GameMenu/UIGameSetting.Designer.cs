using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:21b8323b-32f6-4969-b92e-18605204ae6f
	public partial class UIGameSetting
	{
		public const string Name = "UIGameSetting";
		
		[SerializeField]
		private TMPro.TMP_Dropdown Dropdown;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private UnityEngine.UI.Toggle showUnitHp;
		[SerializeField]
		private UnityEngine.UI.Toggle showUnitTroops;
		[SerializeField]
		private UnityEngine.UI.Toggle showUnitMorale;
		[SerializeField]
		private UnityEngine.UI.Toggle showUnitFatigue;
		[SerializeField]
		private UnityEngine.UI.Toggle showMovementPoints;
		
		private UIGameSettingData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Dropdown = null;
			leaveButton = null;
			showUnitHp = null;
			showUnitTroops = null;
			showUnitMorale = null;
			showUnitFatigue = null;
			showMovementPoints = null;
			
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
