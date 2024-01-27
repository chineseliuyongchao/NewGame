using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IFamilySystem : ISystem
    {
        /// <summary>
        /// 初始化家族通用数据
        /// </summary>
        void InitFamilyCommonData(TextAsset textAsset);

        /// <summary>
        /// 初始化角色通用数据
        /// </summary>
        void InitRoleCommonData(TextAsset textAsset);

        /// <summary>
        /// 添加一位新的角色
        /// </summary>
        /// <param name="roleData"></param>
        /// <returns></returns>
        int AddNewRole(RoleData roleData);
    }
}