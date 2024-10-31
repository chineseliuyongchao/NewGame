using System;
using Fight.Game.Unit;
using Fight.Model;
using QFramework;

namespace Fight.Game.AI
{
    /// <summary>
    /// 移动直至目标进入攻击范围
    /// </summary>
    public class UnitAiMoveInAttackRange : BaseUnitBehaviorAiSingle
    {
        public int targetUnitId;
        private int _endIndex;
        private Action<bool> _behaviorEnd;

        public override void StartBehavior(Action<bool> behaviorEnd)
        {
            _behaviorEnd = behaviorEnd;
            UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            UnitController targetUnitController = this.GetModel<IFightVisualModel>().AllUnit[targetUnitId];
            _endIndex = targetUnitController.unitData.currentPosIndex;
            unitController.Move(_endIndex, BehaviorEnd, MoveOnceEnd);
        }

        private bool MoveOnceEnd(int index)
        {
            return true;
        }

        public override void BehaviorEnd()
        {
            _behaviorEnd(true);
        }
    }
}