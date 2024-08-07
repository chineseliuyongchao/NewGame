using Fight.Enum;

namespace Fight.Events
{
    public struct ArmsMoveEvent
    {
        public BattleType BattleType;
        public int EndIndex;
    }
}