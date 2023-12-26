using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class TownSystem : AbstractSystem, ITownSystem
    {
        protected override void OnInit()
        {
        }

        public void InitTownCommonData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<ITownModel>().TownCommonData);
        }
    }
}