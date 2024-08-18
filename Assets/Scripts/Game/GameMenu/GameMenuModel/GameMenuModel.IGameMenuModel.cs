using System.Collections.Generic;
using QFramework;

// ReSharper disable once CheckNamespace
namespace Game.GameMenu
{
    public interface IGameMenuModel : IModel
    {
        public int Language { get; set; }

        /// <summary>
        /// 所有兵种通用数据
        /// </summary>
        public Dictionary<int, ArmDataType> ARMDataTypes { get; set; }

        /// <summary>
        /// 回到游戏菜单次数
        /// </summary>
        public int RevertMenuTime { get; set; }
    }
}