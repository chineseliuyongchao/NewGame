namespace Fight.Game.AI
{
    /// <summary>
    /// 军队开始回合时的行为ai
    /// </summary>
    public abstract class LegionStartRoundAi : BaseLegionBehaviorAi
    {
        protected LegionStartRoundAi(BaseLegionAi baseLegionAi) : base(baseLegionAi)
        {
        }

        /// <summary>
        /// 军队开始回合
        /// </summary>
        public abstract void Operation();
    }
}