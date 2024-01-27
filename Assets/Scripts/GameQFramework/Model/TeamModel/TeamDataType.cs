using System;

namespace GameQFramework
{
    /// <summary>
    /// 所有队伍的数据，不同存档不同
    /// </summary>
    [Serializable]
    public class TeamData
    {
        /// <summary>
        /// 将领角色id
        /// </summary>
        public int generalRoleId;

        /// <summary>
        /// 队伍人数
        /// </summary>
        public int number;
    }

    /// <summary>
    /// 队伍状态
    /// </summary>
    public enum TeamType
    {
        /// <summary>
        /// 在聚落驻扎
        /// </summary>
        HUT_TOWN,

        /// <summary>
        /// 在野外驻扎
        /// </summary>
        HUT_FIELD,

        /// <summary>
        /// 移动至聚落
        /// </summary>
        MOVE_TO_TOWN,

        /// <summary>
        /// 移动至队伍
        /// </summary>
        MOVE_TO_TEAM,

        /// <summary>
        /// 巡逻
        /// </summary>
        PATROL
    }
}