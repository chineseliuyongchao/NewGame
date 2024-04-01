using QFramework;
using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationCommand : AbstractCommand
    {
        private readonly Vector2 _selectPos;
        private readonly int _townId;

        public SelectMapLocationCommand(Vector2 selectPos, int townId)
        {
            this._selectPos = selectPos;
            this._townId = townId;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new SelectMapLocationEvent(_selectPos, _townId));
        }
    }
}