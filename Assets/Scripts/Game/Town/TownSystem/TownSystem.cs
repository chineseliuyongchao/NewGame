using System.Collections.Generic;
using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Game.Town
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

        public void InitTownNoStorageData()
        {
            Dictionary<int, TownData> townDataS = this.GetModel<ITownModel>().TownData;
            List<int> key = new List<int>(townDataS.Keys);
            for (int i = 0; i < key.Count; i++)
            {
                TownData townData = townDataS[key[i]];
                TownDataNoStorage noStorage = new TownDataNoStorage();
                ComputeProsperity(townData.storage, noStorage);
                townData.noStorage = noStorage;
            }
        }

        public void UpdateTownNoStorageData(TownData townData)
        {
            ComputeProsperity(townData.storage, townData.noStorage);
        }

        /// <summary>
        /// 计算繁荣度
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="noStorage"></param>
        private void ComputeProsperity(TownDataStorage storage, TownDataNoStorage noStorage)
        {
            int workShopRevenue = 1000; //暂定工场收入
            int revenue = workShopRevenue + DailyGrainYield(storage.farmlandNum) * TownConstant.GRAIN_PRICE;
            noStorage.prosperity = (int)(storage.GetPopulation() * TownConstant.POPULATION_GRAIN_CONSUME +
                                         revenue * TownConstant.INCOME_PROSPERITY_COEFFICIENT);
            Debug.Log("聚落繁荣度：" + storage.name + "  " + noStorage.prosperity);
        }

        public void InitTownNode(TownNode townNode)
        {
            _townNode = townNode;
        }

        public ConscriptionData Conscription(int townId)
        {
            return _townNode.Conscription(townId);
        }

        public int DailyGrainYield(int farmlandNum)
        {
            return farmlandNum * TownConstant.FARMLAND_OUTPUT;
        }
    }
}