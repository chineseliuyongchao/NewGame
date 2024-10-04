using System.Collections.Generic;
using Fight.Game.Unit;
using JetBrains.Annotations;
using QFramework;

namespace Fight.Model
{
    /**
     * 存放战斗中和表现有直接关系的数据
     */
    public interface IFightVisualModel : IModel
    {
        /// <summary>
        /// 通过单位id获取战场位置id
        /// </summary>
        Dictionary<int, int> UnitIdToIndexDictionary { get; }

        /// <summary>
        /// 通过战场位置id获取单位id
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

        /// <summary>
        /// 战斗阶段状态
        /// </summary>
        public FightType FightType { get; set; }

        /// <summary>
        /// 是否在玩家的回合内
        /// </summary>
        public bool InPlayerAction { get; set; }
    }
}