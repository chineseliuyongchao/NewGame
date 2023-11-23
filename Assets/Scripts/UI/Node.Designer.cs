using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:45fe8931-5d72-4536-9e1d-7709c69f85c1
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
