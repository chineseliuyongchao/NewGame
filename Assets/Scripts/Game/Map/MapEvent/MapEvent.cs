using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationEvent
    {
        public Vector2 selectPos;
        public readonly int townId;

        public SelectMapLocationEvent(Vector2 selectPos, int townId)
        {
            this.selectPos = selectPos;
            this.townId = townId;
        }
    }
}