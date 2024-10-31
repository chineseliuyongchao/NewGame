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

    /// <summary>
    /// 战斗的攻击行为
    /// </summary>
    public enum FightAttackType
    {
        /// <summary>
        /// 没有行为
        /// </summary>
        NONE,

        /// <summary>
        /// 进攻
        /// </summary>
        ADVANCE,

        /// <summary>
        /// 射击
        /// </summary>
        SHOOT,

        /// <summary>
        /// 持续进攻
        /// </summary>
        SUSTAIN_ADVANCE,

        /// <summary>
        /// 持续射击
        /// </summary>
        SUSTAIN_SHOOT,

        /// <summary>
        /// 冲击
        /// </summary>
        CHARGE
    }

    /// <summary>
    /// 单位状态
    /// </summary>
    public enum UnitType
    {
        /// <summary>
        /// 正常，后续可能会拆成多种状态
        /// </summary>
        NORMAL,

        /// <summary>
        /// 崩溃
        /// </summary>
        COLLAPSE,

        /// <summary>
        /// 全军覆没
        /// </summary>
        DIE
    }

    public enum AiBehaviorType
    {
        /// <summary>
        /// 移动直至目标进入攻击范围
        /// </summary>
        MOVE_IN_ATTACK_RANGE,
    }
}