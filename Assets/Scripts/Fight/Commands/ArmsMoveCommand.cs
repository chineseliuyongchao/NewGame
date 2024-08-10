using Fight.Events;
using Fight.Game;
using Fight.Scenes;
using QFramework;

namespace Fight.Commands
{
    public class ArmsMoveCommand : AbstractCommand
    {
        private readonly int _endIndex;

        public ArmsMoveCommand(int endIndex)
        {
            _endIndex = endIndex;
        }

        protected override void OnExecute()
        {
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
            if (fightGameModel.FocusController == null)
            {
                return;
            }
            
            int index = fightGameModel.ArmsIdToIndexDictionary[fightGameModel.FocusController.id];
            fightGameModel.ArmsIdToIndexDictionary[fightGameModel.FocusController.id] = _endIndex;
            if (!fightGameModel.IndexToArmsIdDictionary.Remove(index))
            {
                fightGameModel.IndexToArmsIdDictionary.Remove(fightGameModel.FocusController.fightCurrentIndex);
            }

            fightGameModel.IndexToArmsIdDictionary[_endIndex] = fightGameModel.FocusController.id;
            fightGameModel.FocusController.fightCurrentIndex = _endIndex;
            fightGameModel.FocusController.ArmsMoveAction(_endIndex);
            this.SendEvent(new ArmsMoveEvent
            {
                BattleType = FightScene.Ins.currentBattleType,
                EndIndex = _endIndex
            });
        }
    }
}