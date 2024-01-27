using QFramework;

namespace GameQFramework
{
    public interface ITeamSystem : ISystem
    {
        /// <summary>
        /// 添加队伍数据
        /// </summary>
        /// <param name="teamData"></param>
        /// <returns></returns>
        int AddTeam(TeamData teamData);
    }
}