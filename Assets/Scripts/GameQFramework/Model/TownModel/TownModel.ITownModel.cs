using System.Collections.Generic;
using Game.Town;
using QFramework;

namespace GameQFramework
{
    public interface ITownModel : IModel
    {
        /// <summary>
        /// 所有聚落的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值）
        /// </summary>
        public Dictionary<string, TownCommonData> TownCommonData { get; set; }
    }
}