using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Game.GameMenu
{
    public class GameMenuSystem: AbstractSystem, IGameMenuSystem
    {
        protected override void OnInit()
        {
            
        }

        public void InitArmData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IGameMenuModel>().ARMDataTypes);
        }

        public void InitFactionData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IGameMenuModel>().FactionDataTypes);
        }
    }
}