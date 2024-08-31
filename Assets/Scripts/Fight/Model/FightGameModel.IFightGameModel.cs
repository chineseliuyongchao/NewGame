using System.Collections.Generic;
using Fight.Game.Arms;
using JetBrains.Annotations;
using QFramework;

namespace Fight
{
    /**
     * 存放战斗场景中通用属性以及数据
     */
    public interface IFightGameModel : IModel
    {
        /// <summary>
        ///     key：单位id
        ///     value：单位在战斗场景中的位置
        /// </summary>
        public Dictionary<int, int> ArmsIdToIndexDictionary { get; }

        /// <summary>
        ///     key：单位在战斗场景中的位置
        ///     value：单位id
        /// </summary>
        public Dictionary<int, int> IndexToArmsIdDictionary { get; }

        /// <summary>
        ///     key：敌军的专属id
        ///     value：敌军在战斗场景中的位置
        /// </summary>
        public Dictionary<int, int> EnemyIdToIndexDictionary { get; }

        /// <summary>
        ///     key：敌军在战斗场景中的位置
        ///     value：敌军的专属id
        /// </summary>
        public Dictionary<int, int> IndexToEnemyIdDictionary { get; }

        /// <summary>
        /// 当前被选取为焦点的兵种
        /// </summary>
        [CanBeNull]
        public ArmsController FocusController { get; set; }
    }
}