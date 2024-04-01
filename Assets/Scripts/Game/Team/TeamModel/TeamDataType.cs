using System;
using UnityEngine;

namespace Game.Team
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

        /// <summary>
        /// 记录队伍当前位置
        /// </summary>
        public Vector2 pos;

        /// <summary>
        /// 记录队伍当前状态，在游戏中设置状态时不要直接调用Model，要通过调用BaseTeam.SetTeamType方法
        /// </summary>
        public TeamType teamType;

        /// <summary>
        /// 目标聚落的id
        /// </summary>
        public int targetTownId;

        /// <summary>
        /// 所在聚落id
        /// </summary>
        public int townId;
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
        /// 移动至野外
        /// </summary>
        MOVE_TO_FIELD,

        /// <summary>
        /// 巡逻
        /// </summary>
        PATROL
    }
}