using Fight.Game.Arms;
using Fight.Scenes;
using QFramework;

namespace Fight
{
    /// <summary>
    /// 单位移动
    /// </summary>
    public class ArmsMoveCommand : AbstractCommand
    {
        private readonly int _endIndex;

        public ArmsMoveCommand(int endIndex)
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

            int index = fightVisualModel.ArmsIdToIndexDictionary[fightVisualModel.FocusController.armData.unitId];
            fightVisualModel.ArmsIdToIndexDictionary[fightVisualModel.FocusController.armData.unitId] = _endIndex;
            if (!fightVisualModel.IndexToArmsIdDictionary.Remove(index))
            {
                fightVisualModel.IndexToArmsIdDictionary.Remove(fightVisualModel.FocusController.armData.currentPosition);
            }

            fightVisualModel.IndexToArmsIdDictionary[_endIndex] = fightVisualModel.FocusController.armData.unitId;
            fightVisualModel.FocusController.armData.currentPosition = _endIndex;
            fightVisualModel.FocusController.ArmsMoveAction(_endIndex);
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
    /// 兵种聚焦
    /// </summary>
    public class SelectArmsFocusCommand : AbstractCommand
    {
        private readonly int _index;

        public SelectArmsFocusCommand(int index)
        {
            _index = index;
        }

        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            ArmsController currentFocusController = FightScene.Ins.GetArmsControllerByIndex(_index);
            currentFocusController.StartFocusAction();
            if (fightVisualModel.FocusController) fightVisualModel.FocusController.EndFocusAction();
            fightVisualModel.FocusController = currentFocusController;
            this.SendEvent(new SelectArmsFocusEvent(_index));
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public class CancelArmsFocusCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            fightVisualModel.FocusController.EndFocusAction();
            this.SendEvent<CancelArmsFocusEvent>();
            fightVisualModel.FocusController = null;
        }
    }
}