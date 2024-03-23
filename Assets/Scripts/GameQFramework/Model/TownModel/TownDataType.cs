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
            townRoleS = new List<int>();
        }

        public int GetPopulation()
        {
            return malePopulation + femalePopulation;
        }
    }
}