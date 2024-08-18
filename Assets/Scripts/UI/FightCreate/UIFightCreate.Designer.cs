using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:512a98ed-84b2-4f65-92f9-3be31b82e148
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
		
		private UIFightCreateData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			createButton = null;
			leaveButton = null;
			belligerentGroup = null;
			belligerent1Add = null;
			belligerent2Add = null;
			
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
