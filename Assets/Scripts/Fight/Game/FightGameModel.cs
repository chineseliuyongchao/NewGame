using System.Collections.Generic;
using Fight.Game.Arms;
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
        public readonly Dictionary<string, int> FightScenePositionDictionary = new();
        
        /// <summary>
        ///     key：兵种在战斗场景中的位置
        ///     value：兵种的实例
        /// </summary>
        public readonly Dictionary<int, string> FightSceneArmsNameDictionary = new();

        /// <summary>
        /// 当前被选取为焦点的兵种，可能为空
        /// </summary>
        public IObjectArmsController FocusController;

        protected override void OnInit()
        {
            FightScenePositionDictionary.Clear();
            FightSceneArmsNameDictionary.Clear();
            GamePlayerModel gamePlayerModel = this.GetModel<GamePlayerModel>();
            foreach (var info in gamePlayerModel.ArmsInfoDictionary)
            {
                FightScenePositionDictionary[info.Key] = info.Value.RanksIndex;
                FightSceneArmsNameDictionary[info.Value.RanksIndex] = info.Key;
            }
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