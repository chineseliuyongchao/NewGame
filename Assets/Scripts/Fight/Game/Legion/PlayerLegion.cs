using System;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 玩家操控的军队
    /// </summary>
    public class PlayerLegion : BaseLegion
    {
        public override void StartAction(Action<int> action)
        {
            actionEnd = action;
        }
    }
}