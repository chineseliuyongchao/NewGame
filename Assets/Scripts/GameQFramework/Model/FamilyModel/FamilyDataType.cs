using System;
using System.Collections.Generic;

namespace GameQFramework
{
    /// <summary>
    /// 所有家族的通用数据，不同存档不同
    /// </summary>
    [Serializable]
    public class FamilyData
    {
        /// <summary>
        /// 家族名字
        /// </summary>
        public string familyName;

        /// <summary>
        /// 家族财富
        /// </summary>
        public long familyWealth;

        /// <summary>
        /// 家族等级
        /// </summary>
        public int familyLevel;

        /// <summary>
        /// 家族所有角色
        /// </summary>
        public List<int> familyRoleS;

        /// <summary>
        /// 效忠国家编号
        /// </summary>
        public int countryId;

        /// <summary>
        /// 家族领袖编号
        /// </summary>
        public int familyLeaderId;

        public FamilyData(FamilyCommonData familyCommonData)
        {
            familyName = familyCommonData.Name;
            familyWealth = familyCommonData.FamilyWealth;
            familyLevel = familyCommonData.FamilyLevel;
            countryId = familyCommonData.CountryId;
            familyLeaderId = familyCommonData.FamilyLeaderId;
            familyRoleS = new List<int>();
        }
    }

    /// <summary>
    /// 所有角色的通用数据，不同存档不同
    /// </summary>
    [Serializable]
    public class RoleData
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        public string roleName;

        /// <summary>
        /// 角色年龄
        /// </summary>
        public int roleAge;

        /// <summary>
        /// 所在家族编号
        /// </summary>
        public int familyId;

        /// <summary>
        /// 所在聚落编号
        /// </summary>
        public int townId;

        /// <summary>
        /// 人物状态
        /// </summary>
        public RoleType roleType;

        public RoleData()
        {
        }

        public RoleData(RoleCommonData roleCommonData)
        {
            roleName = roleCommonData.Name;
            roleAge = roleCommonData.Age;
            familyId = roleCommonData.FamilyId;
            townId = roleCommonData.TownId;
            roleType = (RoleType)roleCommonData.RoleType;
        }
    }

    /// <summary>
    /// 人物的状态
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 闲置状态
        /// </summary>
        INACTIVE,

        /// <summary>
        /// 担任军队的将军
        /// </summary>
        GENERAL,
    }
}