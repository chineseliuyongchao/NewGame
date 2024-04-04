using Game.Town;
using QFramework;

namespace Game.Team
{
    public interface ITeamSystem : ISystem
    {
        /// <summary>
        /// 添加队伍数据
        /// </summary>
        /// <param name="teamData"></param>
        /// <returns></returns>
        int AddTeam(TeamData teamData);

        /// <summary>
        /// 征兵要花的钱
        /// </summary>
        /// <returns></returns>
        int ConscriptionMoney(SoldierStructure structure);

        /// <summary>
        /// 计算家族的钱是否够征兵，计算出最终结果并且直接扣除家族的钱
        /// </summary>
        /// <param name="structure"></param>
        /// <param name="familyId"></param>
        /// <returns></returns>
        void ComputeMoneyWithConscription(SoldierStructure structure, int familyId);
    }
}