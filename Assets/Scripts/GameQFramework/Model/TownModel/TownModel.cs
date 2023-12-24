using System.Collections.Generic;
using QFramework;

namespace GameQFramework
{
    public class TownModel : AbstractModel, ITownModel
    {
        private Dictionary<string, TownCommonData> _townCommonCommonData;

        protected override void OnInit()
        {
            _townCommonCommonData = new Dictionary<string, TownCommonData>();
        }

        public Dictionary<string, TownCommonData> TownCommonData
        {
            get => _townCommonCommonData;
            set => _townCommonCommonData = value;
        }
    }
}