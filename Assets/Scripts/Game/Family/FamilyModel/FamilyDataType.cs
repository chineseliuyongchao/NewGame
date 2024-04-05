using System;
using System.Collections.Generic;

namespace Game.Family
{
    /// <summary>
    /// 单个聚落的数据
    /// </summary>
    public class FamilyData
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public FamilyDataStorage storage;

        // ReSharper disable once UnassignedField.Global
        public FamilyDataNoStorage noStorage;

        public FamilyData(FamilyDataStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// 所有影响家族财富的都调用该方法，方便调试
        /// </summary>
        /// <param name="addMoney"></param>
        public void UpdateWealth(int addMoney)
        {
            storage.familyWealth += addMoney;
        }
    }

    /// <summary>
    /// 所有家族的数据，不同存档不同
    /// </summary>
    [Serializable]
    public class FamilyDataStorage
    {
        /// <summary>
        /// 家族财富
        /// </summary>
        public long familyWealth;

        /// <summary>
        /// 家族等级
        /// </summary>
        public int familyLevel;

        /// <summary>
        /// 效忠国家编号
        /// </summary>
        public int countryId;

        /// <summary>
        /// 家族领袖编号
        /// </summary>
        public int familyLeaderId;

        /// <summary>
        /// 名字
        /// </summary>
        public List<string> name;

        /// <summary>
        /// 家族所有角色
        /// </summary>
        public List<int> familyRoleS;

        /// <summary>
        /// 家族所有聚落
        /// </summary>
        public List<int> familyTownS;

        public FamilyDataStorage()
        {
        }

        public FamilyDataStorage(FamilyCommonData familyCommonData, FamilyNameData familyNameData)
        {
            familyWealth = familyCommonData.FamilyWealth;
            familyLevel = familyCommonData.FamilyLevel;
            countryId = familyCommonData.CountryId;
            familyLeaderId = familyCommonData.FamilyLeaderId;
            name = new List<string> { familyNameData.Chinese, familyNameData.English };
            familyRoleS = new List<int>();
            familyTownS = new List<int>();
        }
    }

    /// <summary>
    /// 单个家族不需要保存的数据，数据都是由FamilyDataStorage算出来的
    /// </summary>
    public class FamilyDataNoStorage
    {
    }

    /// <summary>
    /// 所有角色的通用数据，不同存档不同
    /// </summary>
    [Serializable]
    public class RoleData
    {
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

        /// <summary>
        /// 名字
        /// </summary>
        public List<string> name;

        public RoleData()
        {
        }

        public RoleData(RoleCommonData roleCommonData, RoleNameData roleNameData)
        {
            roleAge = roleCommonData.Age;
            familyId = roleCommonData.FamilyId;
            townId = roleCommonData.TownId;
            roleType = (RoleType)roleCommonData.RoleType;
            name = new List<string> { roleNameData.Chinese, roleNameData.English };
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
        /// 担任队伍的将军
        /// </summary>
        GENERAL,
    }
}