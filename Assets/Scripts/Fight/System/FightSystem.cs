using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using Fight.Utils;
using Game.FightCreate;
using QFramework;

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
            return data.legionId == Constants.PlayLegionId;
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
    }
}