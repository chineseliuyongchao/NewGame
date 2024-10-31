using Fight.Game.Legion;

namespace Fight.Game.AI
{
    /// <summary>
    /// 简单军队ai
    /// </summary>
    public class LegionEasyAi : BaseLegionAi
    {
        public LegionEasyAi(ComputerLegion computerLegion) : base(computerLegion)
        {
            legionStartRoundAi = new LegionStartRoundEasyAi(this);
        }

        public override void StartRound()
        {
            legionStartRoundAi.Operation();
        }
    }
}