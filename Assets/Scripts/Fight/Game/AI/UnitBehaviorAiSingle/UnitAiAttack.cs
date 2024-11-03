using System;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Game.GameUtils;
using Pathfinding;
using QFramework;
using UnityEngine;

namespace Fight.Game.AI
{
    /// <summary>
    /// 攻击目标
    /// </summary>
    public class UnitAiAttack : BaseUnitBehaviorAiSingle
    {
        public int targetUnitId;
        public FightAttackType fightAttackType;
        private Action<bool> _behaviorEnd;

        public override async void StartBehavior(Action<bool> behaviorEnd)
        {
            _behaviorEnd = behaviorEnd;
            UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            UnitController targetUnitController = this.GetModel<IFightVisualModel>().AllUnit[targetUnitId];
            int nowIndex = unitController.unitData.currentPosIndex;
            int targetIndex = targetUnitController.unitData.currentPosIndex;
            Path nowPath = null;
            await this.GetModel<IAStarModel>().FindNodePath(nowIndex, targetIndex, path => { nowPath = path; });
            if (nowPath.error)
            {
                Debug.LogError("Pathfinding error: " + nowPath.errorLog);
                isEnd = true;
                BehaviorEnd();
                return;
            }

            UnitData unitData = this.GetSystem<IFightSystem>().FindUnit(unitId);
            if (nowPath.vectorPath.Count - 2 >= unitData.armDataType.attackRange)
            {
                isEnd = true;
                BehaviorEnd(); //如果开始行为时攻击范围不够就直接结束
            }

            while (this.GetSystem<IFightComputeSystem>().CheckCanAttack(unitId))
            {
                bool needBreak = false;
                switch (fightAttackType)
                {
                    case FightAttackType.ADVANCE:
                        this.GetSystem<IFightComputeSystem>().AssaultWithRetaliation(unitId, targetUnitId);
                        needBreak = true;
                        break;
                    case FightAttackType.SHOOT:
                        this.GetSystem<IFightComputeSystem>().Shoot(unitId, targetUnitId);
                        needBreak = true;
                        break;
                    case FightAttackType.SUSTAIN_ADVANCE:
                        this.GetSystem<IFightComputeSystem>().AssaultWithRetaliation(unitId, targetUnitId);
                        break;
                    case FightAttackType.SUSTAIN_SHOOT:
                        this.GetSystem<IFightComputeSystem>().Shoot(unitId, targetUnitId);
                        break;
                    case FightAttackType.CHARGE:
                        needBreak = true;
                        break;
                }

                unitController.Attack();
                targetUnitController.Attacked();
                unitController.UpdateType(this.GetSystem<IFightComputeSystem>().UpdateUnitType(unitId));
                targetUnitController.UpdateType(this.GetSystem<IFightComputeSystem>().UpdateUnitType(targetUnitId));
                await DelayManager.Instance.WaitTimeAsync(1);
                if (needBreak)
                {
                    break;
                }
            }

            isEnd = true;
            BehaviorEnd();
        }

        public override void BehaviorEnd()
        {
            _behaviorEnd(isEnd);
        }

        public override ActionType ActionType()
        {
            return Model.ActionType.ATTACK;
        }
    }
}