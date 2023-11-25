using System.Collections.Generic;
using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统对应地图中的每个节点的信息
    /// </summary>
    public class PathfindingMapNode
    {
        /// <summary>
        /// 在地图中的位置
        /// </summary>
        public IntVector2 Pos;

        /// <summary>
        /// 地形
        /// </summary>
        public TerrainType TerrainType;

        /// <summary>
        /// 记录周围所有可以同行的点
        /// </summary>
        public List<PathfindingMapNode> AroundNode;
    }

    /// <summary>
    /// 地形类型
    /// </summary>
    public enum TerrainType
    {
        /// <summary>
        /// 可以通过
        /// </summary>
        CAN_PASS,

        /// <summary>
        /// 不能通过
        /// </summary>
        CANNOT_PASS
    }
}