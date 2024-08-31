using System.Collections.Generic;
using QFramework;

namespace Fight
{
    public class FightModel : AbstractModel, IFightModel
    {
        private Dictionary<int, ArmData> _armDataTypes;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmData>();
        }

        public Dictionary<int, ArmData> ARMDataTypes
        {
            get => _armDataTypes;
            set => _armDataTypes = value;
        }
    }
}