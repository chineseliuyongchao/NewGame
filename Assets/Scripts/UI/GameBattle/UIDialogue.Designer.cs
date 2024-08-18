using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:cadddf82-c205-40de-b9e1-60655440a09d
	public partial class UIDialogue
	{
		public const string Name = "UIDialogue";
		
		[SerializeField]
		private UnityEngine.UI.Image npcDialogBox;
		[SerializeField]
		private UnityEngine.UI.Image playerDialogBox;
		[SerializeField]
		private TMPro.TextMeshProUGUI npcText;
		[SerializeField]
		private TMPro.TextMeshProUGUI playText;
		
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
