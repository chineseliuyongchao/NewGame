using System.Collections.Generic;
using QFramework;

namespace Game.Town
{
    public interface ITownModel : IModel
    {
        /// <summary>
        /// 所有聚落的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值）
        /// </summary>
        public Dictionary<int, TownCommonData> TownCommonData { get; set; }

        /// <summary>
        /// 所有国家的名字数据
        /// </summary>
        public Dictionary<int, TownNameData> TownNameData { get; set; }

        /// <summary>
        /// 记录所有聚落的当前数据，跟随存档变化
        /// </summary>
        public Dictionary<int, TownData> TownData { get; set; }
    }
}