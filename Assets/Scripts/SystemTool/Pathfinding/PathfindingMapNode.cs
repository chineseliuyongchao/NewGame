using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统对应地图中的每个节点的信息
    /// </summary>
    public class PathfindingMapNode
    {
        private IntVector2 _pos;

        /// <summary>
        /// 在地图中的位置
        /// </summary>
        public IntVector2 Pos
        {
            get => _pos;
            set => _pos = value;
        }

        private TerrainType _terrainType;

        /// <summary>
        /// 地形
        /// </summary>
        public TerrainType TerrainType
        {
            get => _terrainType;
            set => _terrainType = value;
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