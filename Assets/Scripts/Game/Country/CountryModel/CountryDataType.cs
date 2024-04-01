using System;
using System.Collections.Generic;

namespace Game.Country
{
    /// <summary>
    /// 单个国家的数据
    /// </summary>
    [Serializable]
    public class CountryData
    {
        /// <summary>
        /// 国家名字
        /// </summary>
        public string name;

        /// <summary>
        /// 财富
        /// </summary>
        public long wealth;

        /// <summary>
        /// 国家等级
        /// </summary>
        public int countryLevel;

        /// <summary>
        /// 国家的所有家族
        /// </summary>
        public List<int> countryFamilyS;

        /// <summary>
        /// 国家的所有聚落
        /// </summary>
        public List<int> countryTownS;

        /// <summary>
        /// 统治家族编号
        /// </summary>
        public int rulingFamilyId;

        public CountryData(CountryCommonData countryCommonData)
        {
            name = countryCommonData.Name;
            wealth = countryCommonData.CountryWealth;
            countryLevel = countryCommonData.CountryLevel;
            rulingFamilyId = countryCommonData.RulingFamilyId;
            countryFamilyS = new List<int>();
            countryTownS = new List<int>();
        }
    }
}