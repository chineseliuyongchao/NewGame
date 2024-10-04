using System.Collections.Generic;
using QFramework;

// ReSharper disable once CheckNamespace
namespace Game.GameMenu
{
    public class GameMenuModel : AbstractModel, IGameMenuModel
    {
        private Dictionary<int, ArmDataType> _armDataTypes;
        private Dictionary<int, FactionDataType> _factionDataTypes;

        private int _revertMenuTime;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmDataType>();
            _factionDataTypes = new Dictionary<int, FactionDataType>();
        }

        public Dictionary<int, ArmDataType> ARMDataTypes
        {
            get => _armDataTypes;
            set => _armDataTypes = value;
        }

        public Dictionary<int, FactionDataType> FactionDataTypes
        {
            get => _factionDataTypes;
            set => _factionDataTypes = value;
        }

        public int RevertMenuTime
        {
            get => _revertMenuTime;
            set => _revertMenuTime = value;
        }
    }
}