using Game.Town;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class TownSystem : AbstractSystem, ITownSystem
    {
        private TownNode _townNode;
        
        protected override void OnInit()
        {
        }

        public void InitTownCommonData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<ITownModel>().TownCommonData);
        }

        public void InitTownNode(TownNode townNode)
        {
            _townNode = townNode;
        }

        public ConscriptionData Conscription(int townId)
        {
            return _townNode.Conscription(townId);
        }
    }
}