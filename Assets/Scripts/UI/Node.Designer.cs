using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:bb0fe346-6c63-45dc-8ab7-ddb0d22146d4
	public partial class Node
	{
		public const string Name = "Node";
		
		[SerializeField]
		public UnityEngine.UI.Button Button;
		
		private NodeData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Button = null;
			
			mData = null;
		}
		
		public NodeData Data
		{
			get
			{
				return mData;
			}
		}
		
		NodeData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new NodeData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
