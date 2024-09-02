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
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            fightGameModel.ArmsIdToIndexDictionary.Clear();
            fightGameModel.IndexToArmsIdDictionary.Clear();
            fightGameModel.EnemyIdToIndexDictionary.Clear();
            fightGameModel.IndexToEnemyIdDictionary.Clear();
            //获取所有战场上的军队数据
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();

            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                foreach (var tmp2 in tmp.allArm)
                {
                    fightGameModel.ArmsIdToIndexDictionary[tmp2.Value.unitId] = tmp2.Value.currentPosition;
                    fightGameModel.IndexToArmsIdDictionary[tmp2.Value.currentPosition] = tmp2.Value.unitId;
                }
            }
        }

        public bool CanWalkableIndex(int index)
        {
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            return !fightGameModel.IndexToEnemyIdDictionary.ContainsKey(index) &&
                   this.GetModel<IAStarModel>().FightGridNodeInfoList[index].WalkableErosion;
        }
    }
}