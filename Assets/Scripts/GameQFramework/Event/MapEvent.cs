
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationEvent
    {
        public Vector2 SelectPos;

        public SelectMapLocationEvent(Vector2 selectPos)
        {
            SelectPos = selectPos;
        }
    }
}