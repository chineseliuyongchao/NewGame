using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:10ee8277-c9c1-4cdd-a17a-5fd22cc92b9a
	public partial class UIFightCreateUnit
	{
		public const string Name = "UIFightCreateUnit";
		
		[SerializeField]
		private UnityEngine.UI.Text armName;
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
