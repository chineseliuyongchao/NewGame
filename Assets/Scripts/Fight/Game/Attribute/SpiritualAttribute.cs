using System;
using UnityAttribute;

namespace Fight.Game.Attribute
{
    [Serializable]
    public class SpiritualAttribute : IAttribute
    {
        /// <summary>
        ///     最高作战意志
        /// </summary>
        [Label("最高作战意志")] public int totalFightWill;

        /// <summary>
        ///     当前作战意志
        /// </summary>
        [Label("当前作战意志")] public int currentFightWill;

        /// <summary>
        ///     总疲劳值
        /// </summary>
        [Label("总疲劳值")] public int totalFatigue;

        /// <summary>
        ///     当前疲劳值
        /// </summary>
        [Label("当前疲劳值")] public int currentFatigue;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}