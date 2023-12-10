using QFramework;
using UnityEngine;
using Utils;

namespace GameQFramework
{
    public interface IMapSystem : ISystem
    {
        /// <summary>
        /// 设置地图物体，游戏开始调用，用于计算位置
        /// </summary>
        void SetMapGameObject(SpriteRenderer map);

        /// <summary>
        /// 获取在地图上面的位置
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        Vector2 GetMapPos(Transform transform);

        /// <summary>
        /// 根据地图位置获取实际位置
        /// </summary>
        /// <param name="transform">当前角色的transform</param>
        /// <param name="mapPos">在地图上面的位置</param>
        /// <returns></returns>
        Vector2 GetMapToRealPos(Transform transform, Vector2 mapPos);

        /// <summary>
        /// 根据地图上的位置计算出网格地图的位置
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        IntVector2 GetGridMapPos(Vector2 pos);

        /// <summary>
        /// 根据网格获取在地图上面的对应位置
        /// </summary>
        /// <returns></returns>
        Vector2 GetGridToMapPos(IntVector2 pos, Position position = Position.CENTER_BOTTOM);
    }
}