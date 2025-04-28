using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:307330b3-b33e-438f-b893-cfcca1659e32
	public partial class UIDialogue
	{
		public const string Name = "UIDialogue";
		
		[SerializeField]
		private UnityEngine.UI.Image npcDialogBox;
		[SerializeField]
		private UnityEngine.UI.Image playerDialogBox;
		[SerializeField]
		private UnityEngine.UI.Text npcText;
		[SerializeField]
		private UnityEngine.UI.Text playText;
		
		private UIDialogueData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			npcDialogBox = null;
			playerDialogBox = null;
			npcText = null;
			playText = null;
			
			mData = null;
		}
		
		public UIDialogueData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIDialogueData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIDialogueData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
