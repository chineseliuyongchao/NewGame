using QFramework;

namespace Game.GameMenu
{
    public interface IGameSettingModel : IModel
    {
        /// <summary>
        /// 语言
        /// </summary>
        public int Language { get; set; }

        /// <summary>
        /// 是否展示血量
        /// </summary>
        public bool ShowUnitHp { get; set; }

        /// <summary>
        /// 是否展示人数
        /// </summary>
        public bool ShowUnitTroops { get; set; }

        /// <summary>
        /// 是否展示作战意志
        /// </summary>
        public bool ShowUnitMorale { get; set; }

        /// <summary>
        /// 是否展示疲劳值
        /// </summary>
        public bool ShowUnitFatigue { get; set; }

        /// <summary>
        /// 是否展示剩余移动力
        /// </summary>
        public bool ShowMovementPoints { get; set; }

        /// <summary>
        /// 在回合中是否自动切换选中兵种
        /// </summary>
        public bool AutomaticSwitchingUnit { get; set; }
    }
}