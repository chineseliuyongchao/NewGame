using System.Collections.Generic;
using Game.GameBase;
using QFramework;

namespace Battle.Town
{
    /// <summary>
    /// 用于存放所有聚落的节点
    /// </summary>
    public class TownNode : BaseGameController
    {
        private BaseTown _townPrefab;
        private Dictionary<int, BaseTown> _towns;

        protected override void OnInit()
        {
            base.OnInit();
            _towns = new Dictionary<int, BaseTown>();
            InitTown();
            this.GetSystem<ITownSystem>().InitTownNode(this);
        }

        /// <summary>
        /// 生成所有城市
        /// </summary>
        private void InitTown()
        {
            _townPrefab = resLoader.LoadSync<Battle.Town.Town>(GamePrefabConstant.TOWN);
            Dictionary<int, TownCommonData> townData = this.GetModel<ITownModel>().TownCommonData;
            foreach (var kvp in townData)
            {
                int key = kvp.Key;
                BaseTown town = Instantiate(_townPrefab, transform);
                town.InitTown(key);
                _towns.Add(key, town);
            }
        }

        public ConscriptionData Conscription(int townId)
        {
            BaseTown baseTown = _towns[townId];
            return baseTown.Conscription();
        }
    }
}