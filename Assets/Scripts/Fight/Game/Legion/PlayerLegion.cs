using System;
using System.Collections.Generic;
using Fight.Command;
using Fight.Event;
using Fight.Model;
using Game.FightCreate;
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
            this.RegisterEvent<EndActionEvent>(e =>
            {
                if (e.isPlayer)
                {
                    this.GetModel<IFightVisualModel>().InPlayerAction = false;
                    this.SendCommand(new CancelUnitFocusCommand());
                    EndRound();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<SelectUnitFocusEvent>(e => { nowUnitController = e.controller; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        public override void StartRound(Action<int> action)
        {
            base.StartRound(action);
            this.GetModel<IFightVisualModel>().InPlayerAction = true;
            this.SendCommand(new StartActionCommand(true));
        }

        protected override void UnitStartRound()
        {
        }

        protected override void UnitEndRound()
        {
            Dictionary<int, UnitData> allUnit = this.GetModel<IFightCreateModel>().AllLegions[legionId].allUnit;
            if (nowUnitIndex >= allUnit.Count - 1)
            {
                EndRound();
            }
            else
            {
                AutomaticSwitchingUnit();
            }
        }

        protected override void UnitEndAction()
        {
            if (this.GetModel<IGameSettingModel>().AutomaticSwitchingUnit)
            {
                base.UnitEndAction();
            }
        }
    }
}