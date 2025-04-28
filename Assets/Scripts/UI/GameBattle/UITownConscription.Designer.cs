using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:cd01fdd5-66ae-496b-b886-5e91083f93c8
	public partial class UITownConscription
	{
		public const string Name = "UITownConscription";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private UnityEngine.UI.Text title;
		[SerializeField]
		private UnityEngine.UI.Text chooseNum;
		[SerializeField]
		private UnityEngine.UI.Slider Slider;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UITownConscriptionData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
			title = null;
			chooseNum = null;
			Slider = null;
			leaveButton = null;
			
			mData = null;
		}
		
		public UITownConscriptionData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownConscriptionData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownConscriptionData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
