using System;
using Utils;

namespace GameQFramework
{
    /// <summary>
    /// 所有家族的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class FamilyCommonData : BaseJsonData
    {
        /// <summary>
        /// 家族财富
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public long FamilyWealth;

        /// <summary>
        /// 家族等级
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FamilyLevel;

        /// <summary>
        /// 效忠国家编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int CountryId;

        /// <summary>
        /// 家族领袖编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FamilyLeaderId;
    }

    /// <summary>
    /// 所有角色的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值），保存在配置json中
    /// </summary>
    [Serializable]
    public class RoleCommonData : BaseJsonData
    {
        /// <summary>
        /// 角色年龄
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int Age;

        /// <summary>
        /// 所在家族编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int FamilyId;

        /// <summary>
        /// 所在聚落编号（游戏开始时所有角色默认在聚落中）
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int TownId;

        /// <summary>
        /// 人物默认状态
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int RoleType;
    }
}