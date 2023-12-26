using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IFamilySystem : ISystem
    {
        /// <summary>
        /// 初始化家族数据
        /// </summary>
        void InitFamilyCommonData(TextAsset textAsset);

        /// <summary>
        /// 初始化角色数据
        /// </summary>
        void InitRoleCommonData(TextAsset textAsset);
    }
}