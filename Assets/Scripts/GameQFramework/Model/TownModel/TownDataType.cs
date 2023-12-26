using System;

namespace GameQFramework
{
    /// <summary>
    /// 单个聚落的数据
    /// </summary>
    [Serializable]
    public class TownData
    {
        /// <summary>
        /// 聚落名字
        /// </summary>
        public string name;

        /// <summary>
        /// 财富
        /// </summary>
        public long wealth;

        /// <summary>
        /// 人口
        /// </summary>
        public int population;

        /// <summary>
        /// 等级
        /// </summary>
        public int level;

        public TownData(TownCommonData townCommonData)
        {
            name = townCommonData.Name;
            wealth = townCommonData.InitWealth;
            population = townCommonData.InitPopulation;
            level = townCommonData.InitLevel;
        }
    }
}