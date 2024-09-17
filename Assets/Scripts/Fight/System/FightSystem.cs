using Fight.Utils;
using Game.FightCreate;
using QFramework;

namespace Fight
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
                    fightVisualModel.UnitIdToIndexDictionary[tmp2.Value.unitId] = tmp2.Value.currentPosition;
                    fightVisualModel.IndexToUnitIdDictionary[tmp2.Value.currentPosition] = tmp2.Value.unitId;
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
    }
}