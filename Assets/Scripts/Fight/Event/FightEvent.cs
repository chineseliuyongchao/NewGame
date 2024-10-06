using Fight.Game.Unit;

namespace Fight.Event
{
    /// <summary>
    /// 单位移动
    /// </summary>
    public struct UnitMoveEvent
    {
        public int endIndex;

        public UnitMoveEvent(int endIndex)
        {
            this.endIndex = endIndex;
        }
    }

    /// <summary>
    /// 单位聚焦
    /// </summary>
    public struct SelectUnitFocusEvent
    {
        public int selectIndex;
        public UnitController controller;

        public SelectUnitFocusEvent(int selectIndex, UnitController controller)
        {
            this.selectIndex = selectIndex;
            this.controller = controller;
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public struct CancelUnitFocusEvent
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

    /// <summary>
    /// 开始回合
    /// </summary>
    public class StartActionEvent
    {
        /// <summary>
        /// 是不是玩家
        /// </summary>
        public readonly bool isPlayer;

        public StartActionEvent(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }
    }

    /// <summary>
    /// 结束回合
    /// </summary>
    public class EndActionEvent
    {
        /// <summary>
        /// 是不是玩家
        /// </summary>
        public readonly bool isPlayer;

        public EndActionEvent(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }
    }
}