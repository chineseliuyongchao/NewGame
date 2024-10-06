using System;
using System.Collections.Generic;
using System.Linq;
using Fight.Command;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 军队基类，不需要加载到游戏物体
    /// </summary>
    public abstract class BaseLegion : BaseGameController
    {
        public int legionId;
        protected Action<int> roundEnd;

        /// <summary>
        /// 军队是否在回合中
        /// </summary>
        protected bool inRound;

        /// <summary>
        /// 当前正在执行回合的单位顺序
        /// </summary>
        protected int nowUnitIndex;

        /// <summary>
        /// 当前正在行动的单位
        /// </summary>
        protected UnitController nowUnitController;

        public virtual void Init(int id)
        {
            legionId = id;
            OnListenEvent();
            inRound = false;
            nowUnitIndex = -1;
        }

        /// <summary>
        /// 开始回合
        /// </summary>
        public virtual void StartRound(Action<int> action)
        {
            inRound = true;
            roundEnd = action;
            nowUnitIndex = -1;
            AutomaticSwitchingUnit();
        }

        /// <summary>
        /// 自动跳转下一个单位
        /// </summary>
        protected void AutomaticSwitchingUnit()
        {
            Dictionary<int, UnitData> allUnit = this.GetModel<IFightCreateModel>().AllLegions[legionId].allUnit;
            nowUnitIndex++;
            int unitId = allUnit.ElementAt(nowUnitIndex).Value.unitId;
            nowUnitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            this.SendCommand(new SelectUnitFocusCommand(nowUnitController));
            UnitStartRound();
        }

        /// <summary>
        /// 单位开始执行回合
        /// </summary>
        protected virtual void UnitStartRound()
        {
        }

        /// <summary>
        /// 单位回合结束
        /// </summary>
        protected virtual void UnitEndRound()
        {
            Dictionary<int, UnitData> allUnit = this.GetModel<IFightCreateModel>().AllLegions[legionId].allUnit;
            if (nowUnitIndex >= allUnit.Count - 1)
            {
                EndRound();
            }
            else
            {
                AutomaticSwitchingUnit();
            }
        }

        /// <summary>
        /// 单位回合中的单次行动结束
        /// </summary>
        protected virtual void UnitEndAction()
        {
            if (!this.GetSystem<IFightComputeSystem>().EnoughMovePoint(nowUnitController.unitData.unitId))
            {
                UnitEndRound();
            }
        }

        /// <summary>
        /// 单位移动
        /// </summary>
        /// <param name="unitIndex">单位id</param>
        /// <param name="endIndex">结束的位置id</param>
        public virtual void UnitMove(int unitIndex, int endIndex)
        {
            Dictionary<int, UnitData> allUnit = this.GetModel<IFightCreateModel>().AllLegions[legionId].allUnit;
            if (allUnit.TryGetValue(unitIndex, out var unitData))
            {
                if (!nowUnitController.Equals(this.GetModel<IFightVisualModel>().AllUnit[unitData.unitId]))
                {
                    Debug.LogError("实际操作的单位和当前选中的单位不一样，实际操作的单位：" + unitData.unitId + "  选中的单位：" +
                                   nowUnitController.unitData.unitId);
                    return;
                }

                nowUnitController.Move(endIndex, UnitEndAction);
            }
        }

        /// <summary>
        /// 单位攻击（此处的攻击是泛指所有的攻击行为）
        /// </summary>
        /// <param name="unitIndex"></param>
        /// <param name="targetUnitIndex"></param>
        public virtual void UnitAttack(int unitIndex, int targetUnitIndex)
        {
            Dictionary<int, UnitController> allUnitController = this.GetModel<IFightVisualModel>().AllUnit;
            if (allUnitController.TryGetValue(unitIndex, out var unitController) &&
                allUnitController.TryGetValue(targetUnitIndex, out var targetUnitController))
            {
                if (!nowUnitController.Equals(
                        this.GetModel<IFightVisualModel>().AllUnit[unitController.unitData.unitId]))
                {
                    Debug.LogError("实际操作的单位和当前选中的单位不一样，实际操作的单位：" + unitController.unitData.unitId + "  选中的单位：" +
                                   nowUnitController.unitData.unitId);
                    return;
                }

                switch (this.GetModel<IFightVisualModel>().FightAttackType)
                {
                    case FightAttackType.ADVANCE:
                        this.GetSystem<IFightComputeSystem>().AssaultWithRetaliation(unitIndex, targetUnitIndex);
                        break;
                    case FightAttackType.SHOOT:
                        this.GetSystem<IFightComputeSystem>().Shoot(unitIndex, targetUnitIndex);
                        break;
                    case FightAttackType.SUSTAIN_ADVANCE:
                        break;
                    case FightAttackType.SUSTAIN_SHOOT:
                        break;
                    case FightAttackType.CHARGE:
                        break;
                }

                unitController.Attack();
                targetUnitController.Attacked();
            }
        }

        /// <summary>
        /// 结束回合
        /// </summary>
        protected virtual void EndRound()
        {
            inRound = false;
            if (roundEnd != null)
            {
                roundEnd(legionId);
                roundEnd = null;
            }
        }
    }
}