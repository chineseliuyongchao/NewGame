using System.Collections.Generic;
using UnityEngine;

namespace Battle.Map
{
    /// <summary>
    /// 寻路系统对应地图中的每个节点的信息
    /// </summary>
    public class PathfindingMapNode
    {
        /// <summary>
        /// 记录地图节点的物理信息
        /// </summary>
        public RectInt nodeRect;

        /// <summary>
        /// 地形
        /// </summary>
        public TerrainType terrainType;

        /// <summary>
        /// 记录周围所有可以通行的点
        /// </summary>
        public Dictionary<int, PathfindingMapNode> aroundNode;
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