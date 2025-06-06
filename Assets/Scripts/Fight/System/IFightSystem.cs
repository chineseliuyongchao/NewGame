﻿using System;
using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using QFramework;

namespace Fight.System
{
    public interface IFightSystem : ISystem
    {
        /// <summary>
        /// 初始化战场数据
        /// </summary>
        void InitFightData();

        /// <summary>
        /// 判断是否可以移动到某个位置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool CanWalkableIndex(int index);

        /// <summary>
        /// 判断单位是不是玩家的
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        bool IsPlayerUnit(int unitId);

        /// <summary>
        /// 根据单位id查找单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        UnitData FindUnit(int unitId);

        /// <summary>
        /// 修改一个单位的位置记录
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="endIndex"></param>
        void UnitChangePos(UnitController controller, int endIndex);

        /// <summary>
        /// 判断可以展示哪些战斗行为按钮（进攻，射击，持续进攻，持续射击，冲击，坚守，转向，撤离战场）
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        List<bool> FightBehaviorButtonShow(int unitId);

        /// <summary>
        /// 获取单位的阵营id
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        int GetCampIdOfUnit(int unitId);

        /// <summary>
        /// 获取一个单位的周围6格的其他单位数据，返回一个列表，表示从该单位的正上方逆时针旋转的其他单位信息
        /// </summary>
        /// <param name="unitController">要计算的单位</param>
        /// <returns></returns>
        List<UnitController> GetUnitsNearUnit(UnitController unitController);

        /// <summary>
        /// 获取一个位置周围的位置，给一个位置的index，会返回这个位置周围位置的index列表
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        List<int> GetPosNearPos(int index);

        /// <summary>
        /// 判断攻击范围是否足够，因为a*底层判断的方法是异步的，所以要用回调
        /// </summary>
        void IsInAttackRange(int unitId, int targetUnitId, Action<bool> res);

        /// <summary>
        /// 检测战斗是否结束
        /// </summary>
        /// <returns></returns>
        bool CheckFightFinish();
    }
}