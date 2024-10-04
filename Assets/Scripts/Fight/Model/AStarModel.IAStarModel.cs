using System.Collections.Generic;
using Pathfinding;
using QFramework;
using UnityEngine;

namespace Fight.Model
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
        /// 给定任意一个坐标，查找坐标最接近的地图节点，返回其节点
        /// </summary>
        /// <returns>离坐标最接近的地图节点</returns>
        GridNodeBase GetGridNode(Vector3 position);

        /// <summary>
        /// 给定任意一个坐标，查找坐标最接近的地图节点，返回其坐标
        /// </summary>
        /// <returns>离坐标最接近的地图节点坐标</returns>
        Vector3 GetGridNodePosition(Vector3 position);

        public Vector3 GetUnitRelayPosition(UnitData unitData);

        /// <summary>
        /// 给定任意一个坐标，查找坐标最接近的地图节点，返回这个节点在我们的规范中的index
        /// </summary>
        /// <returns>我们的规范中的index</returns>
        int GetGridNodeIndexMyRule(Vector3 position);

        /// <summary>
        /// 给定当前所在index和给定任意一个坐标，查找当前位置到达该坐标的所有关键节点，找到后调用相应回调
        /// </summary>
        /// <param name="startIndex">当前index</param>
        /// <param name="position">任意坐标</param>
        /// <param name="callBack">找到路径后的回调</param>
        /// <returns></returns>
        void FindNodePath(int startIndex, Vector3 position, OnPathDelegate callBack);

        void FindNodePath(int startIndex, int endIndex, OnPathDelegate callBack);
    }
}