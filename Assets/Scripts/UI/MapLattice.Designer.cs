using UnityEngine;

namespace UI
{
	// Generate Id:2fa69ed7-3e6e-4d72-a3b7-cb6ef0a72917
	public partial class MapLattice
	{
		public const string Name = "MapLattice";
		
		[SerializeField]
		public UnityEngine.UI.Button button;
		[SerializeField]
		public UnityEngine.UI.Image route;
		
		private MapLatticeData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			button = null;
			route = null;
			
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
