using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:ec88c58f-8f98-4b58-90c3-b8758fe110e4
	public partial class UITownConscription
	{
		public const string Name = "UITownConscription";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private TMPro.TextMeshProUGUI title;
		[SerializeField]
		private TMPro.TextMeshProUGUI chooseNum;
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
