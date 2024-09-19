using System.Collections.Generic;
using Fight.Game.Legion;
using QFramework;

namespace Fight.Model
{
    /// <summary>
    /// 存放战斗中的战斗数据
    /// </summary>
    public interface IFightCoreModel : IModel
    {
        /// <summary>
        /// 战斗阶段状态
        /// </summary>
        public FightType FightType { get; set; }

        /// <summary>
        /// 当前战场上所有的军队
        /// </summary>
        public Dictionary<int, BaseLegion> AllLegion { get; set; }
    }
}