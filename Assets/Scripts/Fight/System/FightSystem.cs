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
            fightVisualModel.ArmsIdToIndexDictionary.Clear();
            fightVisualModel.IndexToArmsIdDictionary.Clear();
            fightVisualModel.EnemyIdToIndexDictionary.Clear();
            fightVisualModel.IndexToEnemyIdDictionary.Clear();
            //获取所有战场上的军队数据
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();

            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                foreach (var tmp2 in tmp.allArm)
                {
                    fightVisualModel.ArmsIdToIndexDictionary[tmp2.Value.unitId] = tmp2.Value.currentPosition;
                    fightVisualModel.IndexToArmsIdDictionary[tmp2.Value.currentPosition] = tmp2.Value.unitId;
                }
            }
        }

        public bool CanWalkableIndex(int index)
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            return !fightVisualModel.IndexToEnemyIdDictionary.ContainsKey(index) &&
                   this.GetModel<IAStarModel>().FightGridNodeInfoList[index].WalkableErosion;
        }
    }
}