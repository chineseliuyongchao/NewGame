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

        public void InitTownCommonData(TextAsset textAsset, TextAsset nameTextAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<ITownModel>().TownCommonData);
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(nameTextAsset, this.GetModel<ITownModel>().TownNameData);
        }

        public void InitTownNoStorageData()
        {
            Dictionary<int, TownData> townDataS = this.GetModel<ITownModel>().TownData;
            List<int> key = new List<int>(townDataS.Keys);
            for (int i = 0; i < key.Count; i++)
            {
                TownData townData = townDataS[key[i]];
                TownDataNoStorage noStorage = new TownDataNoStorage();
                ComputeProsperity(townData, noStorage);
                townData.noStorage = noStorage;
            }
        }

        public void UpdateTownNoStorageData(TownData townData)
        {
            ComputeProsperity(townData, townData.noStorage);
        }

        /// <summary>
        /// 计算繁荣度
        /// </summary>
        /// <param name="townData"></param>
        /// <param name="noStorage"></param>
        private void ComputeProsperity(TownData townData, TownDataNoStorage noStorage)
        {
            int workShopRevenue = 1000; //暂定工场收入
            int revenue = workShopRevenue + DailyGrainYield(townData.storage.farmlandNum) * TownConstant.GRAIN_PRICE;
            noStorage.prosperity = (int)(townData.GetPopulation() * TownConstant.POPULATION_PROSPERITY_COEFFICIENT +
                                         revenue * TownConstant.INCOME_PROSPERITY_COEFFICIENT);
            Debug.Log("聚落繁荣度：" + townData.storage.name + "  " + noStorage.prosperity);
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