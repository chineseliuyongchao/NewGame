using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 记录地图相关的数据
    /// </summary>
    public interface IMapModel : IModel
    {
        /// <summary>
        /// 地图网格数据
        /// </summary>
        PathfindingMap Map { get; set; }

        /// <summary>
        /// 地图尺寸
        /// </summary>
        Vector2 MapSize { get; set; }
    }
}