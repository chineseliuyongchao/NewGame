namespace Fight
{
    /// <summary>
    /// 单位移动
    /// </summary>
    public struct ArmsMoveEvent
    {
        public BattleType battleType;
        public int endIndex;

        public ArmsMoveEvent(BattleType battleType, int endIndex)
        {
            this.battleType = battleType;
            this.endIndex = endIndex;
        }
    }

    /// <summary>
    /// 兵种聚焦
    /// </summary>
    public struct SelectArmsFocusEvent
    {
        public readonly BattleType battleType;
        public int selectIndex;

        public SelectArmsFocusEvent(BattleType battleType, int selectIndex)
        {
            this.battleType = battleType;
            this.selectIndex = selectIndex;
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public struct CancelArmsFocusEvent
    {
    }

    public struct StartWarPreparationsEvent
    {
    }

    public struct EndWarPreparationsEvent
    {
    }

    public struct StartBattleEvent
    {
    }

    public struct EndBattleEvent
    {
    }

    public struct StartPursuitEvent
    {
    }

    public struct EndPursuitEvent
    {
    }

    public struct BattleOverEvent
    {
    }
}