using System;
using System.Threading.Tasks;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Game.GameTest;
using Pathfinding;
using QFramework;
using UnityEngine;

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

        public override async void StartBehavior(Action<bool> behaviorEnd)
        {
            _behaviorEnd = behaviorEnd;
            if (this.GetModel<IGameTestModel>().AINoMove)
            {
                isEnd = true;
                BehaviorEnd();
                return;
            }

            UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            UnitController targetUnitController = this.GetModel<IFightVisualModel>().AllUnit[targetUnitId];
            int nowIndex = unitController.unitData.currentPosIndex;
            _endIndex = targetUnitController.unitData.currentPosIndex;
            Path nowPath = null;
            await this.GetModel<IAStarModel>().FindNodePath(nowIndex, _endIndex, path => { nowPath = path; });
            if (nowPath.error)
            {
                Debug.LogError("Pathfinding error: " + nowPath.errorLog);
                isEnd = true;
                BehaviorEnd();
                return;
            }

            UnitData unitData = this.GetSystem<IFightSystem>().FindUnit(unitId);
            if (nowPath.vectorPath.Count - 2 < unitData.armDataType.attackRange)
            {
                isEnd = true;
                BehaviorEnd(); //如果开始行为时已经到达目的地就直接结束
            }
            else
            {
                await unitController.Move(_endIndex, BehaviorEnd, MoveOnceEnd);
            }
        }

        /// <summary>
        /// 每一步移动结束时调用，判断是否需要继续移动
        /// </summary>
        /// <param name="index">移动到的位置</param>
        /// <returns></returns>
        private async Task<bool> MoveOnceEnd(int index)
        {
            Path nowPath = null;
            await this.GetModel<IAStarModel>().FindNodePath(index, _endIndex, path => { nowPath = path; });
            if (nowPath.error)
            {
                Debug.LogError("Pathfinding error: " + nowPath.errorLog);
                isEnd = true;
                return false;
            }

            UnitData unitData = this.GetSystem<IFightSystem>().FindUnit(unitId);
            if (nowPath.vectorPath.Count - 2 < unitData.armDataType.attackRange)
            {
                isEnd = true;
                return false;
            }

            return true;
        }

        public override void BehaviorEnd()
        {
            _behaviorEnd(isEnd);
        }

        public override ActionType ActionType()
        {
            return Model.ActionType.MOVE;
        }
    }
}