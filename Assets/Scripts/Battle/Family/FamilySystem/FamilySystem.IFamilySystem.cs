using QFramework;
using UnityEngine;

namespace Battle.Family
{
    public interface IFamilySystem : ISystem
    {
        /// <summary>
        /// 初始化家族通用数据
        /// </summary>
        void InitFamilyCommonData(TextAsset textAsset, TextAsset nameTextAsset);

        /// <summary>
        /// 初始化家族中不需要保存的数据
        /// </summary>
        void InitFamilyNoStorageData();

        /// <summary>
        /// 初始化角色通用数据
        /// </summary>
        void InitRoleCommonData(TextAsset textAsset, TextAsset nameTextAsset);

        /// <summary>
        /// 添加一位新的角色
        /// </summary>
        /// <param name="roleData"></param>
        /// <returns></returns>
        int AddNewRole(RoleData roleData);

        /// <summary>
        /// 添加一个新家族
        /// </summary>
        /// <param name="familyData"></param>
        /// <returns></returns>
        int AddNewFamily(FamilyData familyData);
    }
}