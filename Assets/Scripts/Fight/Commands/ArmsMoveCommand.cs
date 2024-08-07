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

            string name = fightGameModel.FocusController.GetName();
            int index = fightGameModel.FightScenePositionDictionary[name];
            fightGameModel.FightScenePositionDictionary[name] = _endIndex;
            if (!fightGameModel.FightSceneArmsNameDictionary.Remove(index))
            {
                fightGameModel.FightSceneArmsNameDictionary.Remove(fightGameModel.FocusController.GetModel()
                    .CurrentIndex);
            }

            fightGameModel.FightSceneArmsNameDictionary[_endIndex] = name;
            fightGameModel.FocusController.ArmsMoveAction(_endIndex);
            fightGameModel.FocusController.GetModel().CurrentIndex = _endIndex;
            this.SendEvent(new ArmsMoveEvent
            {
                BattleType = FightScene.Ins.currentBattleType,
                EndIndex = _endIndex
            });
        }
    }
}