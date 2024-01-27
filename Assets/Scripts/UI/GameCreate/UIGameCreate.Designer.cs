using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:f0512d6f-b5a2-427e-bb13-95144c54c350
	public partial class UIGameCreate
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
		
		private UIGameCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			inputName = null;
			inputAge = null;
			
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
