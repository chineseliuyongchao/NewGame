using Fight.Enum;

namespace Fight.Events
{
    public struct SelectArmsFocusEvent
    {
        public BattleType BattleType;
        public int SelectIndex;
    }
}