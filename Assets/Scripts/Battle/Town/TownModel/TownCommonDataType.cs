using System;
using Game.GameUtils;

namespace Battle.Town
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

        /// <summary>
        /// 农田数量
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FarmlandNum;

        /// <summary>
        /// 农田上限
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FarmlandUpperLimit;

        /// <summary>
        /// 粮仓等级
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int GranaryLevel;

        /// <summary>
        /// 粮食储量
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int GrainReserves;
    }

    /// <summary>
    /// 所有聚落的初始名字数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class TownNameData : BaseNameJsonData
    {
    }
}