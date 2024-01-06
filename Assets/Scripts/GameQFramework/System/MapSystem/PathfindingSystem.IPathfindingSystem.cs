using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IPathfindingSystem : ISystem
    {
        /// <summary>
        /// 寻路
        /// </summary>
        /// <param name="startPos">开始位置</param>
        /// <param name="endPos">结束位置</param>
        /// <param name="map">地图</param>
        /// <returns></returns>
        public PathfindingSingleMessage Pathfinding(Vector2 startPos, Vector2 endPos, PathfindingMap map);

        public PathfindingSingleMessage Pathfinding(Vector2Int startPos, Vector2Int endPos, PathfindingMap map);
    }
}