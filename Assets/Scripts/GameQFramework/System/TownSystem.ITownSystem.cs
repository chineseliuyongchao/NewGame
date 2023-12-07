using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface ITownSystem : ISystem
    {
        /// <summary>
        /// 初始化聚落数据
        /// </summary>
        void InitTownData(TextAsset textAsset);
    }
}