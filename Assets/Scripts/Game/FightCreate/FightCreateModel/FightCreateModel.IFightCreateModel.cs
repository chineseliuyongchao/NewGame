using System.Collections.Generic;
using QFramework;

namespace Game.FightCreate
{
    /// <summary>
    /// 存放创建战斗时参战单位的信息
    /// </summary>
    public interface IFightCreateModel : IModel
    {
        /// <summary>
        /// 记录战场中所有参战军团的信息(军团id，军团信息)
        /// </summary>
        public Dictionary<int, LegionInfo> AllLegions { get; set; }
    }
}