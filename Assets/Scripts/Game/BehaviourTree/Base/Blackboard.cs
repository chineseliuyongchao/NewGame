using System;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 黑板类
    /// </summary>
    [Serializable]
    public class Blackboard
    {
        /// <summary>
        /// 队伍的将领的人物id
        /// </summary>
        public int teamGeneralId;

        /// <summary>
        /// 组建队伍
        /// </summary>
        public Action<int> buildTeam;

        /// <summary>
        /// 目标聚落
        /// </summary>
        public int targetTownId;

        /// <summary>
        /// 移动至聚落
        /// </summary>
        public Action<int> moveToTown;

        /// <summary>
        /// 巡逻
        /// </summary>
        public Action<int> patrol;

        public virtual void Resetting()
        {
            teamGeneralId = 0;
        }
    }
}