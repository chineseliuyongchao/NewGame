using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:52e9cceb-96d5-486d-a511-d560dc1c72e4
	public partial class UIGameSetting
	{
		public const string Name = "UIGameSetting";
		
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private UnityEngine.UI.Button basicSettingButton;
		[SerializeField]
		private UnityEngine.UI.Button fightSettingButton;
		[SerializeField]
		private RectTransform basicSettingGroup;
		[SerializeField]
		private TMPro.TMP_Dropdown Dropdown;
		[SerializeField]
		private RectTransform fightSettingGroup;
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
		[SerializeField]
		private UnityEngine.UI.Toggle automaticSwitchingUnit;
		
		private UIGameSettingData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			leaveButton = null;
			basicSettingButton = null;
			fightSettingButton = null;
			basicSettingGroup = null;
			Dropdown = null;
			fightSettingGroup = null;
			showUnitHp = null;
			showUnitTroops = null;
			showUnitMorale = null;
			showUnitFatigue = null;
			showMovementPoints = null;
			automaticSwitchingUnit = null;
			
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
