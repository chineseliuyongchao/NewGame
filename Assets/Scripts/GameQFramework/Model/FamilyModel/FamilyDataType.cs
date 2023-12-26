using System;
using System.Collections.Generic;

namespace GameQFramework.FamilyModel
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
        public List<RoleData> familyRoleData;

        public FamilyData(FamilyCommonData familyCommonData)
        {
            familyName = familyCommonData.Name;
            familyWealth = familyCommonData.FamilyWealth;
            familyLevel = familyCommonData.FamilyLevel;
            familyRoleData = new List<RoleData>();
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

        public RoleData(RoleCommonData roleCommonData)
        {
            roleName = roleCommonData.Name;
            roleAge = roleCommonData.Age;
        }
    }
}