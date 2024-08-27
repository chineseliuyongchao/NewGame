using System.Collections.Generic;
using QFramework;

namespace Battle.Team
{
    public interface ITeamModel : IModel
    {
        /// <summary>
        /// 队伍数据
        /// </summary>
        public Dictionary<int, TeamData> TeamData { get; set; }
    }
}