using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:b2ea3feb-de5e-40e4-a75e-eafa94365f29
	public partial class UINewFile
	{
		public const string Name = "UINewFile";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private TMPro.TMP_InputField inputFileData;
		[SerializeField]
		private UnityEngine.UI.Button determineButton;
		[SerializeField]
		private UnityEngine.UI.Button cancellationButton;
		
		private UINewFileData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			root = null;
			inputFileData = null;
			determineButton = null;
			cancellationButton = null;
			
			mData = null;
		}
		
		public UINewFileData Data
		{
			get
			{
				return mData;
			}
		}
		
		UINewFileData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UINewFileData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
