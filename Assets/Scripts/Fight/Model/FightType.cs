namespace Fight.Model
{
    /// <summary>
    /// 战斗的各个状态
    /// </summary>
    public enum FightType
    {
        /// <summary>
        /// 战斗准备阶段
        /// </summary>
        WAR_PREPARATIONS,

        /// <summary>
        /// 战斗中
        /// </summary>
        IN_FIGHT,

        /// <summary>
        /// 战斗结算
        /// </summary>
        SETTLEMENT,

        /// <summary>
        /// 战斗结束
        /// </summary>
        FIGHT_OVER
    }
}