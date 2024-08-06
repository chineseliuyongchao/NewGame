using Fight.Enum;
using Fight.Events;
using Fight.Scenes;
using QFramework;


namespace Fight.Commands
{
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
}