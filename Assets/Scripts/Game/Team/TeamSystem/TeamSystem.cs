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
    }
}