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
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            if (fightGameModel.FocusController == null)
            {
                return;
            }

            int index = fightGameModel.ArmsIdToIndexDictionary[fightGameModel.FocusController.armData.unitId];
            fightGameModel.ArmsIdToIndexDictionary[fightGameModel.FocusController.armData.unitId] = _endIndex;
            if (!fightGameModel.IndexToArmsIdDictionary.Remove(index))
            {
                fightGameModel.IndexToArmsIdDictionary.Remove(fightGameModel.FocusController.armData.currentPosition);
            }

            fightGameModel.IndexToArmsIdDictionary[_endIndex] = fightGameModel.FocusController.armData.unitId;
            fightGameModel.FocusController.armData.currentPosition = _endIndex;
            fightGameModel.FocusController.ArmsMoveAction(_endIndex);
            this.SendEvent(new ArmsMoveEvent(FightScene.Ins.currentBattleType, _endIndex));
        }
    }

    /// <summary>
    /// 战斗界面的状态命令，标志着战斗进行了哪个状态，处理相关逻辑并且发送相关消息
    /// </summary>
    public class BattleCommand : AbstractCommand
    {
        private readonly BattleType _type;

        public BattleCommand(BattleType type)
        {
            _type = type;
        }

        protected override void OnExecute()
        {
            FightScene.Ins.currentBattleType = _type;
            switch (_type)
            {
                case BattleType.StartWarPreparations:
                    this.SendEvent<StartWarPreparationsEvent>();
                    break;
                case BattleType.EndWarPreparations:
                    this.SendEvent<EndWarPreparationsEvent>();
                    break;
                case BattleType.StartBattle:
                    this.SendEvent<StartBattleEvent>();
                    break;
                case BattleType.EndBattle:
                    this.SendEvent<EndBattleEvent>();
                    break;
                case BattleType.StartPursuit:
                    this.SendEvent<StartPursuitEvent>();
                    break;
                case BattleType.EndPursuit:
                    this.SendEvent<EndPursuitEvent>();
                    break;
                case BattleType.BattleOver:
                    this.SendEvent<BattleOverEvent>();
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
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            ArmsController currentFocusController = FightScene.Ins.GetArmsControllerByIndex(_index);
            currentFocusController.StartFocusAction();
            if (fightGameModel.FocusController) fightGameModel.FocusController.EndFocusAction();
            fightGameModel.FocusController = currentFocusController;
            this.SendEvent(new SelectArmsFocusEvent(FightScene.Ins.currentBattleType, _index));
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public class CancelArmsFocusCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            if (!fightGameModel.FocusController)
            {
                return;
            }

            fightGameModel.FocusController.EndFocusAction();
            this.SendEvent<CancelArmsFocusEvent>();
            fightGameModel.FocusController = null;
        }
    }
}