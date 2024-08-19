using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:dd40c56c-b618-44ea-aa21-e955f7dff54d
	public partial class UIFightCreate
	{
		public const string Name = "UIFightCreate";
		
		[SerializeField]
		private UnityEngine.UI.Button createButton;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private UnityEngine.RectTransform belligerentGroup;
		[SerializeField]
		private UnityEngine.UI.Button belligerent1Add;
		[SerializeField]
		private UnityEngine.UI.Button belligerent2Add;
		[SerializeField]
		private UnityEngine.UI.Dropdown chooseFight;
		[SerializeField]
		private RectTransform showAllUnit;
		[SerializeField]
		private UnityEngine.UI.Dropdown chooseArm;
		[SerializeField]
		private UnityEngine.UI.Button belligerent3Add;
		
		private UIFightCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			belligerentGroup = null;
			belligerent1Add = null;
			belligerent2Add = null;
			chooseFight = null;
			showAllUnit = null;
			chooseArm = null;
			belligerent3Add = null;
			
			mData = null;
		}
		
		public UIFightCreateData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightCreateData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightCreateData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
