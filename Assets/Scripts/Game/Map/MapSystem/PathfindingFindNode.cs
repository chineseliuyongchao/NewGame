namespace Game.Map
{
    /// <summary>
    /// 单次寻路系统的每个节点
    /// </summary>
    public class PathfindingFindNode
    {
        /// <summary>
        /// 当前节点的信息
        /// </summary>
        public PathfindingMapNode pathfindingMapNode;

        /// <summary>
        /// 当前点到起点的距离
        /// </summary>
        public float lengthToStart;

        /// <summary>
        /// 当前点到终点的距离
        /// </summary>
        public float lengthToEnd;

        /// <summary>
        /// 总共需要的距离
        /// </summary>
        public float totalLength;

        /// <summary>
        /// 当前节点的父级节点
        /// </summary>
        public PathfindingFindNode fatherNode;
    }
}