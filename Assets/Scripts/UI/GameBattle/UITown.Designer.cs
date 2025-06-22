using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:f04eed65-1e81-424d-9472-5f3844919dd8
	public partial class UITown
	{
		public const string Name = "UITown";
		
		[SerializeField]
		private UnityEngine.UI.Text countyName;
		[SerializeField]
		private UnityEngine.UI.Text countyIntroduce;
		[SerializeField]
		private UnityEngine.UI.Text populationValue;
		[SerializeField]
		private UnityEngine.UI.Text foodSituationValue;
		[SerializeField]
		private UnityEngine.UI.Text garrisonValue;
		[SerializeField]
		private UnityEngine.UI.Text prosperityValue;
		[SerializeField]
		private UnityEngine.UI.Text publicOrderValue;
		[SerializeField]
		private UnityEngine.UI.Text loyaltyValue;
		[SerializeField]
		private UnityEngine.UI.Button importantPeople;
		[SerializeField]
		private UnityEngine.UI.Text scaleText;
		[SerializeField]
		private UnityEngine.UI.Button leve;
		[SerializeField]
		private UnityEngine.UI.Text leveText;
		[SerializeField]
		private UnityEngine.UI.Text insidePlotValue;
		[SerializeField]
		private UnityEngine.UI.Button government;
		[SerializeField]
		private UnityEngine.UI.Button governmentWorkshop;
		[SerializeField]
		private UnityEngine.UI.Button barracks;
		[SerializeField]
		private UnityEngine.UI.Button shop;
		[SerializeField]
		private UnityEngine.UI.Button civilianLive;
		[SerializeField]
		private UnityEngine.UI.Button nobleLive;
		[SerializeField]
		private UnityEngine.UI.Button warehouse;
		[SerializeField]
		private UnityEngine.UI.Button leaveButton;
		
		private UITownData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			countyName = null;
			countyIntroduce = null;
			populationValue = null;
			foodSituationValue = null;
			garrisonValue = null;
			prosperityValue = null;
			publicOrderValue = null;
			loyaltyValue = null;
			importantPeople = null;
			scaleText = null;
			leve = null;
			leveText = null;
			insidePlotValue = null;
			government = null;
			governmentWorkshop = null;
			barracks = null;
			shop = null;
			civilianLive = null;
			nobleLive = null;
			warehouse = null;
			leaveButton = null;
			
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
