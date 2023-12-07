using System.Collections.Generic;
using Game.Town;
using QFramework;

namespace GameQFramework
{
    public interface ITownModel : IModel
    {
        /// <summary>
        /// 记录所有聚落数据
        /// </summary>
        public Dictionary<string, Town> TownData { get; set; }
    }
}