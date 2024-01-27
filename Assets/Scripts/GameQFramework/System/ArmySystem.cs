using QFramework;

namespace GameQFramework
{
    public class ArmySystem : AbstractSystem, IArmySystem
    {
        protected override void OnInit()
        {
        }

        public int AddArmy(ArmyData armyData)
        {
            int armyId = this.GetModel<IArmyModel>().ArmyData.Count + 1;
            this.GetModel<IArmyModel>().ArmyData.Add(armyId, armyData);
            return armyId;
        }
    }
}