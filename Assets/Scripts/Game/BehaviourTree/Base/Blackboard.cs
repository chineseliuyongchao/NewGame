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
        /// 军队的将领的人物id
        /// </summary>
        public int armyGeneralId;

        /// <summary>
        /// 组建军队
        /// </summary>
        public Action<int> buildArmy;

        public virtual void Resetting()
        {
            armyGeneralId = 0;
        }
    }
}