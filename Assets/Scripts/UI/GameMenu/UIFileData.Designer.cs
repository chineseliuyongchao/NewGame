using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:37f3ca2e-b819-486a-8655-4151ad428247
	public partial class UIFileData
	{
		public const string Name = "UIFileData";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI fileName;
		[SerializeField]
		private UnityEngine.UI.Button open;
		[SerializeField]
		private UnityEngine.UI.Button cover;
		[SerializeField]
		private UnityEngine.UI.Button delete;
		
		private UIFileDataData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			fileName = null;
			open = null;
			cover = null;
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
