using QFramework;
using UnityEngine;

namespace Game.Town
{
    public interface ITownSystem : ISystem
    {
        /// <summary>
        /// 初始化聚落数据
        /// </summary>
        void InitTownCommonData(TextAsset textAsset);

        /// <summary>
        /// 初始化聚落中不需要保存的数据
        /// </summary>
        void InitTownNoStorageData();

        /// <summary>
        /// 刷新聚落中不需要保存的数据
        /// </summary>
        void UpdateTownNoStorageData(TownData townData);

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

        /// <summary>
        /// 单日粮食产量
        /// </summary>
        /// <returns>农田数量</returns>
        int DailyGrainYield(int farmlandNum);
    }
}