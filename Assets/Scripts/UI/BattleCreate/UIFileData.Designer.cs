using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:b4ae40e7-daff-4ebd-8392-95f815c7c837
	public partial class UIFileData
	{
		public const string Name = "UIFileData";
		
		[SerializeField]
		private UnityEngine.UI.Text fileName;
		[SerializeField]
		private UnityEngine.UI.Button openButton;
		[SerializeField]
		private UnityEngine.UI.Button coverButton;
		[SerializeField]
		private UnityEngine.UI.Button deleteButton;
		
		private UIFileDataData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			fileName = null;
			openButton = null;
			coverButton = null;
			deleteButton = null;
			
			mData = null;
		}
		
		public UIFileDataData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFileDataData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFileDataData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
