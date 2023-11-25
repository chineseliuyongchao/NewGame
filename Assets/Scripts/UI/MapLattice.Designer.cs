using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace UI
{
	// Generate Id:11321cc4-b706-48e3-a188-d735a611aa14
	public partial class MapLattice
	{
		public const string Name = "MapLattice";
		
		[SerializeField]
		public UnityEngine.UI.Button button;
		[SerializeField]
		public UnityEngine.UI.Image route;
		[SerializeField]
		public UnityEngine.UI.Image route1;
		
		private MapLatticeData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			button = null;
			route = null;
			route1 = null;
			mData = null;
		}
		
		public MapLatticeData Data
		{
			get
			{
				return mData;
			}
		}
		
		MapLatticeData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new MapLatticeData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
