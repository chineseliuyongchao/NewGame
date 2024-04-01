using QFramework;
using UnityEngine;

namespace Game.Country
{
    public interface ICountrySystem : ISystem
    {
        /// <summary>
        /// 初始化国家通用数据
        /// </summary>
        /// <param name="textAsset"></param>
        void InitCountryCommonData(TextAsset textAsset);
    }
}