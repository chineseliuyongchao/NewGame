using Game.Town;
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationEvent
    {
        public Vector2 selectPos;
        public readonly BaseTown baseTown;

        public SelectMapLocationEvent(Vector2 selectPos, BaseTown baseTown)
        {
            this.selectPos = selectPos;
            this.baseTown = baseTown;
        }
    }
}