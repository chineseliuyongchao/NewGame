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
        /// 财富
        /// </summary>
        public long wealth;

        /// <summary>
        /// 国家等级
        /// </summary>
        public int countryLevel;

        /// <summary>
        /// 统治家族编号
        /// </summary>
        public int rulingFamilyId;

        /// <summary>
        /// 名字
        /// </summary>
        public List<string> name;

        /// <summary>
        /// 国家的所有家族
        /// </summary>
        public List<int> countryFamilyS;

        /// <summary>
        /// 国家的所有聚落
        /// </summary>
        public List<int> countryTownS;

        public CountryData(CountryCommonData countryCommonData, CountryNameData countryNameData)
        {
            wealth = countryCommonData.CountryWealth;
            countryLevel = countryCommonData.CountryLevel;
            rulingFamilyId = countryCommonData.RulingFamilyId;
            name = new List<string> { countryNameData.Chinese, countryNameData.English };
            countryFamilyS = new List<int>();
            countryTownS = new List<int>();
        }
    }
}