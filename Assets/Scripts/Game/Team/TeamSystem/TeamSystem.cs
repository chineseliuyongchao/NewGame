using Game.Family;
using Game.Town;
using QFramework;

namespace Game.Team
{
    public class TeamSystem : AbstractSystem, ITeamSystem
    {
        protected override void OnInit()
        {
        }

        public int AddTeam(TeamData teamData)
        {
            int teamId = this.GetModel<ITeamModel>().TeamData.Count + 1;
            this.GetModel<ITeamModel>().TeamData.Add(teamId, teamData);
            return teamId;
        }

        public int ConscriptionMoney(SoldierStructure structure)
        {
            return structure.num * 10; //先按十块钱一个人算
        }

        public void ComputeMoneyWithConscription(SoldierStructure structure, int familyId)
        {
            int money = ConscriptionMoney(structure);
            FamilyData familyData = this.GetModel<IFamilyModel>().FamilyData[familyId];
            if (familyData.storage.familyWealth >= money)
            {
                familyData.UpdateWealth(-money);
                return;
            }

            //如果钱不够就默认完全不招兵
            structure.num = 0;
        }
    }
}