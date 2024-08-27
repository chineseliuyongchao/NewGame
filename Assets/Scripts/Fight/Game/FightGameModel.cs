using System.Collections.Generic;
using Fight.Game.Arms;
using JetBrains.Annotations;
using QFramework;

namespace Fight.Game
{
    /**
     * 存放战斗场景中通用属性以及数据
     */
    public class FightGameModel : AbstractModel, ICanGetModel
    {
        /// <summary>
        ///     key：兵种的专属id
        ///     value：兵种在战斗场景中的位置
        /// </summary>
        public readonly Dictionary<int, int> ArmsIdToIndexDictionary = new();

        /// <summary>
        ///     key：兵种在战斗场景中的位置
        ///     value：兵种的专属id
        /// </summary>
        public readonly Dictionary<int, int> IndexToArmsIdDictionary = new();

        /// <summary>
        ///     key：敌军的专属id
        ///     value：敌军在战斗场景中的位置
        /// </summary>
        public readonly Dictionary<int, int> EnemyIdToIndexDictionary = new();
        
        /// <summary>
        ///     key：敌军在战斗场景中的位置
        ///     value：敌军的专属id
        /// </summary>
        public readonly Dictionary<int, int> IndexToEnemyIdDictionary = new();

        /// <summary>
        /// 当前被选取为焦点的兵种
        /// </summary>
        [CanBeNull] public ArmsController FocusController;

        protected override void OnInit()
        {
            ArmsIdToIndexDictionary.Clear();
            IndexToArmsIdDictionary.Clear();
            EnemyIdToIndexDictionary.Clear();
            IndexToEnemyIdDictionary.Clear();
        }

        /// <summary>
        /// 给定一个index，返回这个index是否可以到达
        /// 不可到达的原因有：障碍、已经被其他敌方单位占据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CanWalkableIndex(int index)
        {
            return !IndexToEnemyIdDictionary.ContainsKey(index) &&
                   this.GetModel<AStarModel>().FightGridNodeInfoList[index].WalkableErosion;
        }

        /**
         * debug
         * 暂时先这么写，这个方法要删除
         */
        public void GoFightScene()
        {
            OnInit();
        }
    }
}