using Fight.Enum;
using Fight.Events;
using QFramework;

namespace Fight.Commands
{
    public class BattleCommand : AbstractCommand
    {
        private readonly BattleType _type;

        public BattleCommand(BattleType type)
        {
            _type = type;
        }

        protected override void OnExecute()
        {
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