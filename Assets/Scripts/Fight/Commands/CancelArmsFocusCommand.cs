using Fight.Events;
using Fight.Game;
using QFramework;

namespace Fight.Commands
{
    public class CancelArmsFocusCommand : AbstractCommand
    {
        
        protected override void OnExecute()
        {
            
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
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