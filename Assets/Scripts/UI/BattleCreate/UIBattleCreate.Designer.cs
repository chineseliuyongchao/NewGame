using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:6c7d8b03-3400-4f88-b563-67bd6f7437e4
	public partial class UIBattleCreate
	{
		public const string Name = "UIBattleCreate";
		
		[SerializeField]
		private UnityEngine.UI.Button createButton;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private UnityEngine.UI.InputField inputName;
		[SerializeField]
		private UnityEngine.UI.InputField inputAge;
		[SerializeField]
		private UnityEngine.UI.InputField inputFamilyName;
		
		private UIBattleCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			inputName = null;
			inputAge = null;
			inputFamilyName = null;
			
			mData = null;
		}
		
		public UIBattleCreateData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBattleCreateData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBattleCreateData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
