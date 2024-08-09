using Fight.Events;
using Fight.Game;
using Fight.Game.Arms;
using Fight.Scenes;
using QFramework;

namespace Fight.Commands
{
    public class SelectArmsFocusCommand : AbstractCommand
    {
        private int _index;

        public SelectArmsFocusCommand(int index)
        {
            _index = index;
        }

        protected override void OnExecute()
        {
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
            ObjectArmsController currentFocusController = FightScene.Ins.GetArmsControllerByIndex(_index);
            currentFocusController.StartFocusAction();
            if (fightGameModel.FocusController) fightGameModel.FocusController.EndFocusAction();
            fightGameModel.FocusController = currentFocusController;
            this.SendEvent(new SelectArmsFocusEvent
            {
                BattleType = FightScene.Ins.currentBattleType,
                SelectIndex = _index
            });
        }
    }
}