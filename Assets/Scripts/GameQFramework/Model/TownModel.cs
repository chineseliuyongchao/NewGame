using System.Collections.Generic;
using Game.Town;
using QFramework;

namespace GameQFramework
{
    public class TownModel : AbstractModel, ITownModel
    {
        private Dictionary<string, Town> _townData;

        protected override void OnInit()
        {
            _townData = new Dictionary<string, Town>();
        }

        public Dictionary<string, Town> TownData
        {
            get => _townData;
            set => _townData = value;
        }
    }
}