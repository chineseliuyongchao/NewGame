using System.Collections.Generic;
using QFramework;

namespace Game.GameMenu
{
    public class GameMenuModel : AbstractModel, IGameMenuModel
    {
        private int _language;
        private Dictionary<int, ArmDataType> _armDataTypes;
        private int _revertMenuTime;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmDataType>();
        }

        public int Language
        {
            get => _language;
            set => _language = value;
        }

        public Dictionary<int, ArmDataType> ARMDataTypes
        {
            get => _armDataTypes;
            set => _armDataTypes = value;
        }

        public int RevertMenuTime
        {
            get => _revertMenuTime;
            set => _revertMenuTime = value;
        }
    }
}