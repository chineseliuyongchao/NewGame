using System.Collections.Generic;
using Pathfinding;
using QFramework;
using UnityEngine;

namespace Fight
{
    /**
     * 存放a*的相关表格信息
     */
    public interface IAStarModel : IModel
    {
        public SortedList<int, GridNodeBase> FightGridNodeInfoList { get; }

        /// <summary>
        /// 手动初始化相关数据
        /// </summary>
        void InitStarData();

        /// <summary>
        ///     给定任意一个坐标，查找坐标最接近的地图节点，返回其节点
        /// </summary>
        /// <returns>离坐标最接近的地图节点</returns>
        GridNodeBase GetGridNode(Vector3 position);

        /// <summary>
        ///     给定任意一个坐标，查找坐标最接近的地图节点，返回其坐标
        /// </summary>
        /// <returns>离坐标最接近的地图节点坐标</returns>
        Vector3 GetGridNodePosition(Vector3 position);

        /// <summary>
        ///     给定任意一个坐标，查找坐标最接近的地图节点，返回这个节点在我们的规范中的index
        /// </summary>
        /// <returns>我们的规范中的index</returns>
        int GetGridNodeIndexMyRule(Vector3 position);
    }
}