using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:cc1ddf06-9c72-4403-9718-cbda45a2eb03
	public partial class UIFileData
	{
		public const string Name = "UIFileData";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI fileName;
		[SerializeField]
		private UnityEngine.UI.Button open;
		[SerializeField]
		private UnityEngine.UI.Button delete;
		
		private UIFileDataData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			fileName = null;
			open = null;
			delete = null;
			
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
