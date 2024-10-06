using System;
using Fight.Command;
using Fight.Event;
using Fight.Model;
using Game.GameMenu;
using QFramework;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 玩家操控的军队
    /// </summary>
    public class PlayerLegion : BaseLegion
    {
        protected override void OnListenEvent()
        {
            this.RegisterEvent<EndRoundButtonEvent>(_ =>
            {
                this.GetModel<IFightVisualModel>().InPlayerAction = false;
                this.SendCommand(new CancelUnitFocusCommand());
                EndRound();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<SelectUnitFocusEvent>(e => { nowUnitController = e.controller; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public override void StartRound(Action<int> action)
        {
            base.StartRound(action);
            this.GetModel<IFightVisualModel>().InPlayerAction = true;
            this.SendCommand(new StartRoundCommand(true, nowUnitController.unitData.unitId));
        }

        protected override void UnitStartRound()
        {
            this.SendCommand(new PlayerUnitWaitActionCommand(nowUnitController.unitData.unitId));
        }

        protected override void UnitEndAction()
        {
            this.SendCommand(new PlayerUnitWaitActionCommand(nowUnitController.unitData.unitId));
            if (this.GetModel<IGameSettingModel>().AutomaticSwitchingUnit)
            {
                base.UnitEndAction();
            }
        }

        public override void UnitMove(int unitIndex, int endIndex)
        {
            base.UnitMove(unitIndex, endIndex);
            this.SendCommand(new PlayerUnitActionCommand(nowUnitController.unitData.unitId));
        }

        protected override void EndRound()
        {
            base.EndRound();
            this.SendCommand(new EndRoundCommand(true));
        }
    }
}