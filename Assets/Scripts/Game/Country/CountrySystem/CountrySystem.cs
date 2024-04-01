using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Game.Country
{
    public class CountrySystem : AbstractSystem, ICountrySystem
    {
        protected override void OnInit()
        {
        }

        public void InitCountryCommonData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<ICountryModel>().CountryCommonData);
        }
    }
}