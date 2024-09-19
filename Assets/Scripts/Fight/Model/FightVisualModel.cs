using System.Collections.Generic;
using Fight.Game.Unit;
using JetBrains.Annotations;
using QFramework;

namespace Fight.Model
{
    public class FightVisualModel : AbstractModel, IFightVisualModel
    {
        private Dictionary<int, int> _unitIdToIndexDictionary;
        private Dictionary<int, int> _indexToUnitIdDictionary;
        [CanBeNull] private UnitController _focusController;
        private Dictionary<int, UnitController> _allUnit;

        protected override void OnInit()
        {
            _unitIdToIndexDictionary = new Dictionary<int, int>();
            _indexToUnitIdDictionary = new Dictionary<int, int>();
            _allUnit = new Dictionary<int, UnitController>();
        }

        public Dictionary<int, int> UnitIdToIndexDictionary => _unitIdToIndexDictionary;
        public Dictionary<int, int> IndexToUnitIdDictionary => _indexToUnitIdDictionary;

        public UnitController FocusController
        {
            get => _focusController;
            set => _focusController = value;
        }

        public Dictionary<int, UnitController> AllUnit
        {
            get => _allUnit;
            set => _allUnit = value;
        }
    }
}