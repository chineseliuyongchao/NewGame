using System.Collections.Generic;
using QFramework;

namespace Fight
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
        /// 当前战斗的所有单位数据
        /// </summary>
        public Dictionary<int, ArmData> ARMDataTypes { get; set; }
    }
}