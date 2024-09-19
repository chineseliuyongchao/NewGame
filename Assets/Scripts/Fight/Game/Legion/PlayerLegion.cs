using System;
using Fight.Command;
using Fight.Event;
using QFramework;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 玩家操控的军队
    /// </summary>
    public class PlayerLegion : BaseLegion
    {
        public override void StartAction(Action<int> action)
        {
            this.SendCommand(new StartActionCommand(true));
            actionEnd = action;
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<EndActionEvent>(e =>
            {
                if (e.isPlayer)
                {
                    this.SendCommand(new CancelUnitFocusCommand());
                    EndAction();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}