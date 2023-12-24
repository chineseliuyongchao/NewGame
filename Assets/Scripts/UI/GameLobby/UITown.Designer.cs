using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:cd6eb2ad-fe74-4140-8ca3-0d23804d1a1e
	public partial class UITown
	{
		public const string Name = "UITown";
		
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		[SerializeField]
		private TMPro.TextMeshProUGUI townName;
		[SerializeField]
		private TMPro.TextMeshProUGUI wealthValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI populationValue;
		[SerializeField]
		private TMPro.TextMeshProUGUI levelValue;
		
		private UITownData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			leaveButton = null;
			townName = null;
			wealthValue = null;
			populationValue = null;
			levelValue = null;
			
			mData = null;
		}
		
		public UITownData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITownData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITownData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
