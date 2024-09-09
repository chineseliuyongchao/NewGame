namespace Fight
{
    /// <summary>
    /// 单位移动
    /// </summary>
    public struct ArmsMoveEvent
    {
        public int endIndex;

        public ArmsMoveEvent(int endIndex)
        {
            this.endIndex = endIndex;
        }
    }

    /// <summary>
    /// 兵种聚焦
    /// </summary>
    public struct SelectArmsFocusEvent
    {
        public int selectIndex;

        public SelectArmsFocusEvent(int selectIndex)
        {
            this.selectIndex = selectIndex;
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public struct CancelArmsFocusEvent
    {
    }

    /// <summary>
    /// 开始战斗准备阶段
    /// </summary>
    public struct WarPreparationsEvent
    {
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    public struct InFightEvent
    {
    }

    /// <summary>
    /// 开始结算
    /// </summary>
    public struct SettlementEvent
    {
    }

    /// <summary>
    /// 战斗结束
    /// </summary>
    public struct FightOverEvent
    {
    }
}