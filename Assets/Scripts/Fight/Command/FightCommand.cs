using Fight.Game;
using QFramework;
using UnityEngine;

namespace Fight
{
    /// <summary>
    /// 单位移动
    /// </summary>
    public class UnitMoveCommand : AbstractCommand
    {
        private readonly int _endIndex;

        public UnitMoveCommand(int endIndex)
        {
            _endIndex = endIndex;
        }

        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (fightVisualModel.FocusController == null)
            {
                return;
            }

            int index = fightVisualModel.UnitIdToIndexDictionary[fightVisualModel.FocusController.unitData.unitId];
            fightVisualModel.UnitIdToIndexDictionary[fightVisualModel.FocusController.unitData.unitId] = _endIndex;
            if (!fightVisualModel.IndexToUnitIdDictionary.Remove(index))
            {
                fightVisualModel.IndexToUnitIdDictionary.Remove(
                    fightVisualModel.FocusController.unitData.currentPosition);
            }

            fightVisualModel.IndexToUnitIdDictionary[_endIndex] = fightVisualModel.FocusController.unitData.unitId;
            fightVisualModel.FocusController.unitData.currentPosition = _endIndex;
            fightVisualModel.FocusController.UnitMoveAction(_endIndex);
        }
    }

    /// <summary>
    /// 战斗界面的状态命令，标志着战斗进行了哪个状态，处理相关逻辑并且发送相关消息
    /// </summary>
    public class FightCommand : AbstractCommand
    {
        private readonly FightType _type;

        public FightCommand(FightType type)
        {
            _type = type;
        }

        protected override void OnExecute()
        {
            this.GetModel<IFightCoreModel>().FightType = _type;
            switch (_type)
            {
                case FightType.WAR_PREPARATIONS:
                    this.SendEvent<WarPreparationsEvent>();
                    break;
                case FightType.IN_FIGHT:
                    this.SendEvent<InFightEvent>();
                    break;
                case FightType.SETTLEMENT:
                    this.SendEvent<SettlementEvent>();
                    break;
                case FightType.FIGHT_OVER:
                    this.SendEvent<FightOverEvent>();
                    break;
            }
        }
    }

    /// <summary>
    /// 单位聚焦
    /// </summary>
    public class SelectUnitFocusCommand : AbstractCommand
    {
        private readonly int _index;

        public SelectUnitFocusCommand(int index)
        {
            _index = index;
        }

        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            UnitController currentFocusController = null;
            if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(_index, out int id))
            {
                currentFocusController = this.GetModel<IFightVisualModel>().AllUnit[id];
            }

            if (currentFocusController != null)
            {
                currentFocusController.StartFocusAction();
                if (fightVisualModel.FocusController) fightVisualModel.FocusController.EndFocusAction();
                fightVisualModel.FocusController = currentFocusController;
                this.SendEvent(new SelectUnitFocusEvent(_index));
            }
            else
            {
                Debug.LogError("找不到兵种，位置是：" + _index);
            }
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public class CancelUnitFocusCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            fightVisualModel.FocusController.EndFocusAction();
            this.SendEvent<CancelUnitFocusEvent>();
            fightVisualModel.FocusController = null;
        }
    }
}