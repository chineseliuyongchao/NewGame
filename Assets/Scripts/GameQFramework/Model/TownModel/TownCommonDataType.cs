using System;
using Utils;

namespace GameQFramework
{
    /// <summary>
    /// 所有聚落的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class TownCommonData : BaseJsonData
    {
        /// <summary>
        /// 位置
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public float[] TownPos;

        /// <summary>
        /// 默认财富
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public long InitWealth;

        /// <summary>
        /// 默认人口
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int InitPopulation;

        /// <summary>
        /// 默认等级
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int InitLevel;
    }
}