﻿using System.Collections.Generic;
using QFramework;

namespace Battle.Family
{
    public interface IFamilyModel : IModel
    {
        /// <summary>
        /// 所有家族的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值）
        /// </summary>
        public Dictionary<int, FamilyCommonData> FamilyCommonData { get; set; }

        /// <summary>
        /// 所有家族的名字数据
        /// </summary>
        public Dictionary<int, FamilyNameData> FamilyNameData { get; set; }

        /// <summary>
        /// 所有角色的通用数据（部分信息所有存档共用，不会改变。剩余数据作为新存档的默认值）
        /// </summary>
        public Dictionary<int, RoleCommonData> RoleCommonData { get; set; }

        /// <summary>
        /// 所有角色的名字数据
        /// </summary>
        public Dictionary<int, RoleNameData> RoleNameData { get; set; }

        /// <summary>
        /// 游戏当前所有家族信息
        /// </summary>
        public Dictionary<int, FamilyData> FamilyData { get; set; }

        /// <summary>
        /// 游戏当前所有角色信息
        /// </summary>
        public Dictionary<int, RoleData> RoleData { get; set; }
    }
}