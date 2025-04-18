﻿using System.Collections.Generic;
using Fight.Model;
using Pathfinding;
using QFramework;

namespace Fight.System
{
    public interface IFightComputeSystem : ISystem
    {
        /// <summary>
        /// 进入战斗场景后自动计算所有军队的单位位置
        /// </summary>
        void ComputeUnitPos();

        /// <summary>
        /// 一次有反击的进攻，相当于a对b攻击，b也有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="unitAId">单位a的id</param>
        /// <param name="unitBId">单位b的id</param>
        void AssaultWithRetaliation(int unitAId, int unitBId);

        /// <summary>
        /// 一次没有反击的进攻，相当于a对b攻击，b没有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="unitAId">单位a的id</param>
        /// <param name="unitBId">单位b的id</param>
        void AssaultNoRetaliation(int unitAId, int unitBId);

        /// <summary>
        /// 一次射击
        /// </summary>
        /// <param name="unitAId"></param>
        /// <param name="unitBId"></param>
        void Shoot(int unitAId, int unitBId);

        /// <summary>
        /// 单位移动前计算移动力够不够，够的话同时扣除移动力
        /// </summary>
        /// <param name="unitId"></param>
        bool MoveOnce(int unitId);

        /// <summary>
        /// 检查是否可以进攻（泛指所有攻击种类）
        /// </summary>
        /// <returns></returns>
        bool CheckCanAttack(int unitId);

        /// <summary>
        /// 判断是否还有足够的行动点数
        /// </summary>
        /// <returns></returns>
        bool EnoughMovePoint(int unitId, ActionType actionType);

        /// <summary>
        /// 获取各个军队在同一回合的行动顺序
        /// </summary>
        /// <returns></returns>
        List<int> LegionOrder();

        /// <summary>
        /// 刷新单位状态
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        UnitType UpdateUnitType(int unitId);

        /// <summary>
        /// 周围单位崩溃影响作战意志
        /// </summary>
        /// <param name="unitData"></param>
        /// <param name="isOur">是否是友方的单位</param>
        void AroundUnitCollapseChangeMorale(UnitData unitData, bool isOur);

        /// <summary>
        /// 临近单位影响作战意志
        /// </summary>
        /// <param name="unitData"></param>
        void NearUnitChangeMorale(UnitData unitData);

        /// <summary>
        /// 疲劳值改变（一个军队的所有单位结束回合以后计算）
        /// </summary>
        void ChangeFatigue(UnitData unitData);

        /// <summary>
        /// 判断攻击范围够不够
        /// </summary>
        /// <returns></returns>
        bool CheckAttackRange(Path path, UnitData unitData);
    }
}