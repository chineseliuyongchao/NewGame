using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:8852031e-273a-4231-95bf-ebedc82b9bb6
	public partial class UIFileData
	{
		public const string Name = "UIFileData";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI fileName;
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
