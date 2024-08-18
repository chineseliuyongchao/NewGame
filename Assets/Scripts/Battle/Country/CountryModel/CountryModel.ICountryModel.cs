using System.Collections.Generic;
using QFramework;

namespace Battle.Country
{
    public interface ICountryModel : IModel
    {
        /// <summary>
        /// 所有国家的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值）
        /// </summary>
        public Dictionary<int, CountryCommonData> CountryCommonData { get; set; }

        /// <summary>
        /// 所有国家的名字数据
        /// </summary>
        public Dictionary<int, CountryNameData> CountryNameData { get; set; }

        /// <summary>
        /// 游戏当前所有国家信息
        /// </summary>
        public Dictionary<int, CountryData> CountryData { get; set; }
    }
}