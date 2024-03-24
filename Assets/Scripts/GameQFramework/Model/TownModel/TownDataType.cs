using System;
using System.Collections.Generic;

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
        /// 男性人口
        /// </summary>
        public int malePopulation;

        /// <summary>
        /// 女性人口
        /// </summary>
        public int femalePopulation;

        /// <summary>
        /// 等级
        /// </summary>
        public int level;

        /// <summary>
        /// 聚落中所有的角色
        /// </summary>
        public List<int> townRoleS;

        /// <summary>
        /// 所属国家编号
        /// </summary>
        public int countryId;

        /// <summary>
        /// 所属家族编号（目前默认每个聚落都会被分封给国家中的某个家族）
        /// </summary>
        public int familyId;

        /// <summary>
        /// 默认民兵数量
        /// </summary>
        public int militiaNum;

        /// <summary>
        /// 农田数量
        /// </summary>
        public int farmlandNum;

        /// <summary>
        /// 农田上限
        /// </summary>
        public int farmlandUpperLimit;

        /// <summary>
        /// 粮仓等级
        /// </summary>
        public int granaryLevel;

        /// <summary>
        /// 粮食储量
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int grainReserves;

        public TownData(TownCommonData townCommonData)
        {
            name = townCommonData.Name;
            wealth = townCommonData.InitWealth;
            malePopulation = townCommonData.InitMalePopulation;
            femalePopulation = townCommonData.InitFemalePopulation;
            level = townCommonData.InitLevel;
            countryId = townCommonData.CountryId;
            familyId = townCommonData.FamilyId;
            militiaNum = townCommonData.MilitiaNum;
            farmlandNum = townCommonData.FarmlandNum;
            farmlandUpperLimit = townCommonData.FarmlandUpperLimit;
            granaryLevel = townCommonData.GranaryLevel;
            grainReserves = townCommonData.GrainReserves;
            townRoleS = new List<int>();
        }

        public int GetPopulation()
        {
            return malePopulation + femalePopulation;
        }

        /// <summary>
        /// 所有修改人口的操作走同一方法，便于debug
        /// </summary>
        /// <param name="addNum"></param>
        public void UpdateMalePopulation(int addNum)
        {
            malePopulation += addNum;
        }

        public void UpdateFemalePopulation(int addNum)
        {
            femalePopulation += addNum;
        }
    }
}