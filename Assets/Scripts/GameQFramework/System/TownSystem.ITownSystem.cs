using Game.Town;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface ITownSystem : ISystem
    {
        /// <summary>
        /// 初始化聚落数据
        /// </summary>
        void InitTownCommonData(TextAsset textAsset);

        /// <summary>
        /// 初始化聚落节点
        /// </summary>
        /// <param name="townNode"></param>
        void InitTownNode(TownNode townNode);

        /// <summary>
        /// 征兵
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        ConscriptionData Conscription(int townId);
    }
}