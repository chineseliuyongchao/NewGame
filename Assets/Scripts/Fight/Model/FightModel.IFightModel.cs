using System.Collections.Generic;
using QFramework;

namespace Fight
{
    public interface IFightModel : IModel
    {
        /// <summary>
        /// 当前战斗的所有单位数据
        /// </summary>
        public Dictionary<int, ArmData> ARMDataTypes { get; set; }
    }
}