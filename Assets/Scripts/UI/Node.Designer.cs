using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:bffa73d4-93d9-4589-8221-95efa63dd15e
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
