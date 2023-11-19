namespace UI
{
	// Generate Id:659b8013-628e-4923-892b-c95f7ac90306
	public partial class Node
	{
		public const string Name = "Node";
		
		
		private NodeData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
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
