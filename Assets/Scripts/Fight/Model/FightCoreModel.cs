using System.Collections.Generic;
using Fight.Game.Legion;
using QFramework;

namespace Fight.Model
{
    public class FightCoreModel : AbstractModel, IFightCoreModel
    {
        private FightType _fightType;
        private Dictionary<int, BaseLegion> _allLegion;

        protected override void OnInit()
        {
            _allLegion = new Dictionary<int, BaseLegion>();
        }

        public FightType FightType
        {
            get => _fightType;
            set => _fightType = value;
        }

        public Dictionary<int, BaseLegion> AllLegion
        {
            get => _allLegion;
            set => _allLegion = value;
        }
    }
}