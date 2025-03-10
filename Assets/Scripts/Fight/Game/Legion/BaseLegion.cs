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

namespace Fight.Game.Legion
{
    /// <summary>
    /// 军队基类，不需要加载到游戏物体
    /// </summary>
    public abstract class BaseLegion : BaseGameController
    {
        public LegionInfo legionInfo;
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
        /// 当前正在执行回合的单位id
        /// </summary>
        protected int nowUnitId;

        /// <summary>
        /// 当前正在行动的单位
        /// </summary>
        public UnitController nowUnitController;

        public virtual void Init(LegionInfo info)
        {
            this.legionInfo = info;
            inRound = false;
            nowUnitIndex = -1;
            nowUnitId = -1;
        }

        /// <summary>
        /// 开始回合
        /// </summary>
        public virtual void StartRound(Action<int> action)
        {
            inRound = true;
            roundEnd = action;
            nowUnitIndex = -1;
            nowUnitId = -1;
            AutomaticSwitchingUnit();
        }

        /// <summary>
        /// 自动跳转下一个单位
        /// </summary>
        protected void AutomaticSwitchingUnit()
        {
            Dictionary<int, UnitData> allUnit =
                this.GetModel<IFightCreateModel>().AllLegions[legionInfo.legionId].allUnit;
            nowUnitIndex++;
            nowUnitId = allUnit.ElementAt(nowUnitIndex).Value.unitId;
            nowUnitController = this.GetModel<IFightVisualModel>().AllUnit[nowUnitId];
            if (nowUnitController.unitData.UnitType == UnitType.NORMAL)
            {
                this.SendCommand(new SelectUnitFocusCommand(nowUnitController));
                UnitStartRound();
            }
            else
            {
                UnitEndRound();
            }
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
        public virtual void UnitEndRound()
        {
            Dictionary<int, UnitData> allUnit =
                this.GetModel<IFightCreateModel>().AllLegions[legionInfo.legionId].allUnit;
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
        public virtual void UnitEndAction()
        {
            if (!this.GetSystem<IFightComputeSystem>()
                    .EnoughMovePoint(nowUnitController.unitData.unitId, ActionType.NONE))
            {
                UnitEndRound();
            }
        }

        /// <summary>
        /// 结束回合
        /// </summary>
        protected virtual void EndRound()
        {
            inRound = false;
            Dictionary<int, UnitData> allUnit =
                this.GetModel<IFightCreateModel>().AllLegions[legionInfo.legionId].allUnit;
            foreach (var unit in allUnit.Values)
            {
                this.GetSystem<IFightComputeSystem>().ChangeFatigue(unit);
            }

            if (roundEnd != null)
            {
                roundEnd(legionInfo.legionId);
                roundEnd = null;
            }
        }
    }
}