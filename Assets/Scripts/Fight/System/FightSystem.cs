using System;
using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using Fight.Utils;
using Game.FightCreate;
using QFramework;
using UnityEngine;

namespace Fight.System
{
    public class FightSystem : AbstractSystem, IFightSystem
    {
        protected override void OnInit()
        {
        }

        public void InitFightData()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            fightVisualModel.UnitIdToIndexDictionary.Clear();
            fightVisualModel.IndexToUnitIdDictionary.Clear();
            //获取所有战场上的军队数据
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();

            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                foreach (var tmp2 in tmp.allUnit)
                {
                    fightVisualModel.UnitIdToIndexDictionary[tmp2.Value.unitId] = tmp2.Value.currentPosIndex;
                    fightVisualModel.IndexToUnitIdDictionary[tmp2.Value.currentPosIndex] = tmp2.Value.unitId;
                }
            }
        }

        public bool CanWalkableIndex(int index)
        {
            return this.GetModel<IAStarModel>().FightGridNodeInfoList[index].WalkableErosion;
        }

        public bool IsPlayerUnit(int unitId)
        {
            UnitData data = FindUnit(unitId);
            return data.legionId == Constants.PLAY_LEGION_ID;
        }

        public UnitData FindUnit(int unitId)
        {
            foreach (var legion in this.GetModel<IFightCreateModel>().AllLegions.Values)
            {
                foreach (var unit in legion.allUnit)
                {
                    if (unit.Value.unitId == unitId)
                    {
                        return unit.Value;
                    }
                }
            }

            return null;
        }

        public void UnitChangePos(UnitController controller, int endIndex)
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            int index = controller.unitData.currentPosIndex;
            fightVisualModel.UnitIdToIndexDictionary[controller.unitData.unitId] = endIndex;
            if (!fightVisualModel.IndexToUnitIdDictionary.Remove(index))
            {
                fightVisualModel.IndexToUnitIdDictionary.Remove(controller.unitData.currentPosIndex);
            }

            fightVisualModel.IndexToUnitIdDictionary[endIndex] = controller.unitData.unitId;
            controller.unitData.currentPosIndex = endIndex;
        }

        public List<bool> FightBehaviorButtonShow(int unitId)
        {
            return new List<bool> { true, true, true, true, true, true, true, true };
        }

        public int GetBelligerentsIdOfUnit(int unitId)
        {
            UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[unitId];
            int legionId = unitController.unitData.legionId;
            return this.GetModel<IFightCreateModel>().AllLegions[legionId].belligerentsId;
        }

        public List<UnitController> GetUnitsNearUnit(UnitController unitController)
        {
            int index = unitController.unitData.currentPosIndex;
            List<UnitController> result = new List<UnitController>();
            int width = AStarModel.WorldNodeWidth;
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            //从最上方开始逆时针旋转
            int index1 = index + width;
            ObtainUnitController(fightVisualModel, index1, result);
            if (index % width != 0)
            {
                ObtainUnitController(fightVisualModel, index1 - 1, result);
                ObtainUnitController(fightVisualModel, index - 1, result);
            }

            if (index >= width)
            {
                ObtainUnitController(fightVisualModel, index - width, result);
            }

            if ((index + 1) % width != 0)
            {
                ObtainUnitController(fightVisualModel, index + 1, result);
                ObtainUnitController(fightVisualModel, index1 + 1, result);
            }

            return result;
        }

        public List<int> GetPosNearPos(int index)
        {
            List<int> res = new List<int>();
            int width = AStarModel.WorldNodeWidth;
            int index1 = index + width;
            res.Add(index1);
            if (index % width != 0)
            {
                res.Add(index1 - 1);
                res.Add(index - 1);
            }

            if (index >= width)
            {
                res.Add(index - width);
            }

            if ((index + 1) % width != 0)
            {
                res.Add(index + 1);
                res.Add(index1 + 1);
            }

            return res;
        }

        public void IsInAttackRange(int unitId, int targetUnitId, Action<bool> res)
        {
            Dictionary<int, UnitController> allUnitController = this.GetModel<IFightVisualModel>().AllUnit;
            if (allUnitController.TryGetValue(unitId, out var unitController) &&
                allUnitController.TryGetValue(targetUnitId, out var targetUnitController))
            {
                this.GetModel<IAStarModel>().FindNodePath(unitController.unitData.currentPosIndex,
                    targetUnitController.unitData.currentPosIndex, path =>
                    {
                        if (path.error)
                        {
                            Debug.LogError("Pathfinding error: " + path.errorLog);
                            return;
                        }

                        res(this.GetSystem<IFightComputeSystem>().CheckAttackRange(path, unitController.unitData));
                    });
            }
        }

        public bool CheckFightFinish()
        {
            bool canFinish = false;
            bool belligerentFail = true;
            List<int> allLegionKey = new List<int>(this.GetModel<IFightCreateModel>().AllLegions.Keys);
            //玩家阵营的单位是不是都崩溃或者全军覆没了
            for (int i = 0; i < allLegionKey.Count; i++)
            {
                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[allLegionKey[i]];
                if (legionInfo.belligerentsId != Constants.BELLIGERENT1)
                {
                    continue;
                }

                List<int> allUnitKey = new List<int>(legionInfo.allUnit.Keys);
                for (int j = 0; j < allUnitKey.Count; j++)
                {
                    UnitData unitData = legionInfo.allUnit[allUnitKey[j]];
                    if (unitData.UnitType == UnitType.NORMAL)
                    {
                        belligerentFail = false;
                    }
                }
            }

            if (belligerentFail)
            {
                canFinish = true;
            }

            belligerentFail = true;
            //玩家敌对阵营的单位是不是都崩溃或者全军覆没了
            for (int i = 0; i < allLegionKey.Count; i++)
            {
                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[allLegionKey[i]];
                if (legionInfo.belligerentsId != Constants.BELLIGERENT2)
                {
                    continue;
                }

                List<int> allUnitKey = new List<int>(legionInfo.allUnit.Keys);
                for (int j = 0; j < allUnitKey.Count; j++)
                {
                    UnitData unitData = legionInfo.allUnit[allUnitKey[j]];
                    if (unitData.UnitType == UnitType.NORMAL)
                    {
                        belligerentFail = false;
                    }
                }
            }

            if (belligerentFail)
            {
                canFinish = true;
            }

            return canFinish;
        }

        private void ObtainUnitController(IFightVisualModel fightVisualModel, int index, List<UnitController> list)
        {
            if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out int value) &&
                fightVisualModel.AllUnit.TryGetValue(value, out UnitController controller))
            {
                list.Add(controller);
            }
        }
    }
}