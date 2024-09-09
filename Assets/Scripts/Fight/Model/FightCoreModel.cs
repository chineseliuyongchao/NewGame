using System.Collections.Generic;
using QFramework;

namespace Fight
{
    public class FightCoreModel : AbstractModel, IFightCoreModel
    {
        private FightType _fightType;
        private Dictionary<int, ArmData> _armDataTypes;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmData>();
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
    }
}