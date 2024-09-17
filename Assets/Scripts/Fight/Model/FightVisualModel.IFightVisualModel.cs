using System.Collections.Generic;
using Fight.Game;
using JetBrains.Annotations;
using QFramework;

namespace Fight
{
    /**
     * 存放战斗中和表现有直接关系的数据
     */
    public interface IFightVisualModel : IModel
    {
        /// <summary>
        ///     key：单位id
        ///     value：单位在战斗场景中的位置
        /// </summary>
        Dictionary<int, int> UnitIdToIndexDictionary { get; }

        /// <summary>
        ///     key：单位在战斗场景中的位置
        ///     value：单位id
        /// </summary>
        Dictionary<int, int> IndexToUnitIdDictionary { get; }

        /// <summary>
        /// 当前被选取为焦点的兵种
        /// </summary>
        [CanBeNull]
        UnitController FocusController { get; set; }

        /// <summary>
        /// 存放战场上所有的单位（key：单位id，value：单位对象）
        /// </summary>
        public Dictionary<int, UnitController> AllUnit { get; set; }
    }
}