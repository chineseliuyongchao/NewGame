using Game.Town;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    /// <summary>
    /// 鼠标点击地图
    /// </summary>
    public class SelectMapLocationCommand : AbstractCommand
    {
        private readonly Vector2 _selectPos;
        private readonly BaseTown _baseTown;

        public SelectMapLocationCommand(Vector2 selectPos, BaseTown baseTown)
        {
            this._selectPos = selectPos;
            this._baseTown = baseTown;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new SelectMapLocationEvent(_selectPos, _baseTown));
        }
    }
}