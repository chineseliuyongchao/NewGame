using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:a6f07df6-b1e8-46e6-aa65-8585e76fb946
	public partial class UINewFile
	{
		public const string Name = "UINewFile";
		
		[SerializeField]
		private RectTransform root;
		[SerializeField]
		private UnityEngine.UI.InputField inputFileData;
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
