using System;

namespace GameQFramework
{
    /// <summary>
    /// 所有军队的数据，不同存档不同
    /// </summary>
    [Serializable]
    public class ArmyData
    {
        /// <summary>
        /// 将领角色id
        /// </summary>
        public int generalRoleId;

        /// <summary>
        /// 军队人数
        /// </summary>
        public int number;

        public ArmyData()
        {
        }
    }

    /// <summary>
    /// 军队状态
    /// </summary>
    public enum ArmyType
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
        /// 移动至军队
        /// </summary>
        MOVE_TO_ARMY,

        /// <summary>
        /// 巡逻
        /// </summary>
        PATROL
    }
}