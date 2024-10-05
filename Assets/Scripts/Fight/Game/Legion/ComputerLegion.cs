using System.Collections.Generic;
using DG.Tweening;
using Fight.Model;
using Game.FightCreate;
using QFramework;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 电脑操控的军队
    /// </summary>
    public class ComputerLegion : BaseLegion
    {
        protected override void UnitStartRound()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(2);
            sequence.AppendCallback(UnitEndRound);
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
    }
}