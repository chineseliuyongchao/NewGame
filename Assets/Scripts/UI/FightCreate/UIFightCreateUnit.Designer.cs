using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:4e04b076-4e57-41e1-ba18-b14614e8794e
	public partial class UIFightCreateUnit
	{
		public const string Name = "UIFightCreateUnit";
		
		[SerializeField]
		private TMPro.TextMeshProUGUI armName;
		[SerializeField]
		private UnityEngine.UI.Button delete;
		
		private UIFightCreateUnitData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			armName = null;
			delete = null;
			
			mData = null;
		}
		
		public UIFightCreateUnitData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIFightCreateUnitData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIFightCreateUnitData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
