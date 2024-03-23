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
        /// 默认男性人口
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int InitMalePopulation;

        /// <summary>
        /// 默认女性人口
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int InitFemalePopulation;

        /// <summary>
        /// 默认等级
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int InitLevel;

        /// <summary>
        /// 所属国家编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int CountryId;

        /// <summary>
        /// 所属家族编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FamilyId;

        /// <summary>
        /// 默认民兵数量
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int MilitiaNum;
    }
}