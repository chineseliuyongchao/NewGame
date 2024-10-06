using DG.Tweening;

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
    }
}