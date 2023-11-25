namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 单次寻路系统的每个节点
    /// </summary>
    public class PathfindingFindNode
    {
        /// <summary>
        /// 当前节点的信息
        /// </summary>
        public PathfindingMapNode PathfindingMapNode;

        /// <summary>
        /// 当前点到起点的距离
        /// </summary>
        public float LengthToStart;

        /// <summary>
        /// 当前点到终点的距离
        /// </summary>
        public float LengthToEnd;

        /// <summary>
        /// 总共需要的距离
        /// </summary>
        public float TotalLength;

        /// <summary>
        /// 当前节点的父级节点
        /// </summary>
        public PathfindingFindNode FatherNode;
    }
}