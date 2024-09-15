using System.Collections.Generic;
using Fight.Game.Legion;
using QFramework;

namespace Fight
{
    public class FightCoreModel : AbstractModel, IFightCoreModel
    {
        private FightType _fightType;
        private Dictionary<int, ArmData> _armDataTypes;
        private Dictionary<int, BaseLegion> _allLegion;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmData>();
            _allLegion = new Dictionary<int, BaseLegion>();
        }

        public FightType FightType
        {
            get => _fightType;
            set => _fightType = value;
        }

        public Dictionary<int, ArmData> ARMDataTypes
        {
            get => _armDataTypes;
            set => _armDataTypes = value;
        }

        public Dictionary<int, BaseLegion> AllLegion
        {
            get => _allLegion;
            set => _allLegion = value;
        }
    }
}