﻿using System;
using Game.GameUtils;

namespace Battle.Country
{
    /// <summary>
    /// 所有国家的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class CountryCommonData : BaseJsonData
    {
        /// <summary>
        /// 国家财富
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public long CountryWealth;

        /// <summary>
        /// 国家等级
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int CountryLevel;

        /// <summary>
        /// 统治家族编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int RulingFamilyId;
    }

    /// <summary>
    /// 所有国家的初始名字数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class CountryNameData : BaseNameJsonData
    {
    }
}