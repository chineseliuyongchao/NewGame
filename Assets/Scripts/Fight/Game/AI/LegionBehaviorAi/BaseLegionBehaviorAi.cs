using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Game.GameBase;
using QFramework;

namespace Fight.Game.AI
{
    /// <summary>
    /// 军队ai行为的基类，一个军队ai可能会有多个ai行为
    /// </summary>
    public abstract class BaseLegionBehaviorAi : IController
    {
        protected readonly BaseLegionAi baseLegionAi;

        protected BaseLegionBehaviorAi(BaseLegionAi baseLegionAi)
        {
            this.baseLegionAi = baseLegionAi;
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        /// <summary>
        /// 获取最近的敌方单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public UnitController ProximateEnemy(int unitId)
        {
            // 获取当前单位的控制器和位置索引
            UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            int startPosIndex = unitController.unitData.currentPosIndex;

            // 初始化一个队列用于广度优先遍历
            Queue<int> positionQueue = new Queue<int>();
            HashSet<int> visitedPositions = new HashSet<int>(); // 记录访问过的位置避免重复
            positionQueue.Enqueue(startPosIndex);
            visitedPositions.Add(startPosIndex);

            while (positionQueue.Count > 0)
            {
                int currentIndex = positionQueue.Dequeue();

                // 检查当前位置是否有敌方单位
                UnitController enemyUnit = FindEnemyAtPosition(currentIndex, unitId);
                if (enemyUnit != null)
                {
                    // 找到最近的敌方单位，返回
                    return enemyUnit;
                }

                // 获取当前位置周围的位置索引列表
                List<int> nearbyPositions = this.GetSystem<IFightSystem>().GetPosNearPos(currentIndex);
                foreach (int nearbyIndex in nearbyPositions)
                {
                    // 只将未访问过的位置添加到队列中
                    if (!visitedPositions.Contains(nearbyIndex))
                    {
                        visitedPositions.Add(nearbyIndex);
                        positionQueue.Enqueue(nearbyIndex);
                    }
                }
            }

            // 没有找到敌人，返回 null
            return null;
        }

        /// <summary>
        /// 检查给定位置是否有敌方单位
        /// </summary>
        /// <param name="positionIndex"></param>
        /// <param name="currentUnitId"></param>
        /// <returns>如果找到敌方单位则返回，否则返回 null</returns>
        private UnitController FindEnemyAtPosition(int positionIndex, int currentUnitId)
        {
            IFightVisualModel fightModel = this.GetModel<IFightVisualModel>();
            // 检查该位置是否有单位
            foreach (var unit in fightModel.AllUnit.Values)
            {
                if (unit.unitData.currentPosIndex == positionIndex &&
                    this.GetSystem<IFightSystem>().GetBelligerentsIdOfUnit(unit.unitData.unitId) !=
                    this.GetSystem<IFightSystem>().GetBelligerentsIdOfUnit(currentUnitId))
                {
                    return unit; // 找到敌方单位
                }
            }

            return null; // 该位置没有敌方单位
        }
    }
}