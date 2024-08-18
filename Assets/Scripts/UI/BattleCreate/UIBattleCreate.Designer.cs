using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:3fd33e05-64e0-426c-95e6-51b91622e79c
	public partial class UIBattleCreate
	{
		public const string Name = "UIGameCreate";
		
		[SerializeField]
		private UnityEngine.UI.Button createButton;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private TMPro.TMP_InputField inputName;
		[SerializeField]
		private TMPro.TMP_InputField inputAge;
		[SerializeField]
		private TMPro.TMP_InputField inputFamilyName;
		
		private UIGameCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			inputName = null;
			inputAge = null;
			inputFamilyName = null;
			
			mData = null;
		}
		
		public UIGameCreateData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameCreateData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameCreateData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
