using QFramework;
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationCommand : AbstractCommand
    {
        private Vector2 _selectPos;

        public SelectMapLocationCommand(Vector2 selectPos)
        {
            this._selectPos = selectPos;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new SelectMapLocationEvent(_selectPos));
        }
    }
}