namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 单次寻路系统的每个节点
    /// </summary>
    public class PathfindingFindNode
    {
        private PathfindingMapNode _pathfindingMapNode;

        /// <summary>
        /// 当前节点的信息
        /// </summary>
        public PathfindingMapNode PathfindingMapNode
        {
            get => _pathfindingMapNode;
            set => _pathfindingMapNode = value;
        }

        private float _lengthToStart;

        /// <summary>
        /// 当前点到起点的距离
        /// </summary>
        public float LengthToStart
        {
            get => _lengthToStart;
            set => _lengthToStart = value;
        }

        private float _lengthToEnd;

        /// <summary>
        /// 当前点到终点的距离
        /// </summary>
        public float LengthToEnd
        {
            get => _lengthToEnd;
            set => _lengthToEnd = value;
        }

        private float _totalLength;

        /// <summary>
        /// 总共需要的距离
        /// </summary>
        public float TotalLength
        {
            get => _totalLength;
            set => _totalLength = value;
        }

        private PathfindingFindNode _fatherNode;

        /// <summary>
        /// 当前节点的父级节点
        /// </summary>
        public PathfindingFindNode FatherNode
        {
            get => _fatherNode;
            set => _fatherNode = value;
        }
    }
}