using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:3bc712f7-9ecf-4435-9ce0-24f2c613dbf9
	public partial class TimeNode
	{
		public const string Name = "TimeNode";
		
		[SerializeField]
		public UnityEngine.UI.Text timeView;
		
		private TimeNodeData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			timeView = null;
			
			mData = null;
		}
		
		public TimeNodeData Data
		{
			get
			{
				return mData;
			}
		}
		
		TimeNodeData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TimeNodeData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
