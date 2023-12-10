using Game.Town;
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationEvent
    {
        public Vector2 SelectPos;
        public BaseTown BaseTown;

        public SelectMapLocationEvent(Vector2 selectPos, BaseTown baseTown)
        {
            SelectPos = selectPos;
            BaseTown = baseTown;
        }
    }
}