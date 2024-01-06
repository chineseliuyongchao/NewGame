using System.Collections.Generic;
using UnityEngine;

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
        public Vector2Int pos;

        /// <summary>
        /// 节点大小
        /// </summary>
        public Vector2Int size;

        /// <summary>
        /// 地形
        /// </summary>
        public TerrainType terrainType;

        /// <summary>
        /// 记录周围所有可以通行的点
        /// </summary>
        public Dictionary<int, PathfindingMapNode> aroundNode;

        public Vector2Int PosCenter()
        {
            return new Vector2Int(pos.x + size.x / 2, pos.y + size.y / 2);
        }
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