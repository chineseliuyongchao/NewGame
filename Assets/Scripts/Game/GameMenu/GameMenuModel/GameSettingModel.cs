using QFramework;

namespace Game.GameMenu
{
    public class GameSettingModel : AbstractModel, IGameSettingModel
    {
        private int _language;

        private bool _showUnitHp = true;

        private bool _showUnitTroops = true;

        private bool _showUnitMorale = true;

        private bool _showUnitFatigue;

        private bool _showMovementPoints;

        protected override void OnInit()
        {
        }

        public int Language
        {
            get => _language;
            set => _language = value;
        }

        public bool ShowUnitHp
        {
            get => _showUnitHp;
            set => _showUnitHp = value;
        }

        public bool ShowUnitTroops
        {
            get => _showUnitTroops;
            set => _showUnitTroops = value;
        }

        public bool ShowUnitMorale
        {
            get => _showUnitMorale;
            set => _showUnitMorale = value;
        }

        public bool ShowUnitFatigue
        {
            get => _showUnitFatigue;
            set => _showUnitFatigue = value;
        }

        public bool ShowMovementPoints
        {
            get => _showMovementPoints;
            set => _showMovementPoints = value;
        }
    }
}