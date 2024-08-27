namespace Battle.BattleBase
{
    /// <summary>
    /// 有关时间的常量
    /// </summary>
    public abstract class GameTimeConstant
    {
        /// <summary>
        /// 游戏初始年
        /// </summary>
        public const int INIT_YEAR = 467;

        /// <summary>
        /// 游戏初始月
        /// </summary>
        public const int INIT_MONTH = 9;

        /// <summary>
        /// 游戏初始日
        /// </summary>
        public const int INIT_DAY = 4;

        /// <summary>
        /// 游戏初始时辰
        /// </summary>
        public const int INIT_TIME = 1;

        /// <summary>
        /// 现实秒换算游戏时
        /// </summary>
        public const float CONVERT_TIME = 1.5f;

        /// <summary>
        /// 时辰换算日
        /// </summary>
        public const int TIME_CONVERT_DAY = 12;

        /// <summary>
        /// 日换算月
        /// </summary>
        public const int DAY_CONVERT_MONTH = 30;

        /// <summary>
        /// 月换算年
        /// </summary>
        public const int MONTH_CONVERT_YEAR = 12;
    }

    /// <summary>
    /// 游戏预制体相关变量
    /// </summary>
    public abstract class GamePrefabConstant
    {
        /// <summary>
        /// 聚落预制体
        /// </summary>
        public const string TOWN = "Town";

        /// <summary>
        /// 用于调试地图的预制体
        /// </summary>
        public const string MESH_PREFAB = "MeshPrefab";

        /// <summary>
        /// 家族预制体
        /// </summary>
        public const string FAMILY = "Family";

        /// <summary>
        /// 地图节点预制体
        /// </summary>
        public const string MAP_NODE = "MapNode";

        /// <summary>
        /// 聚落节点预制体
        /// </summary>
        public const string TONN_NODE = "TownNode";

        /// <summary>
        /// 玩家队伍预制体
        /// </summary>
        public const string PLAYER_TEAM = "PlayerTeam";

        /// <summary>
        /// ai队伍预制体
        /// </summary>
        public const string TEAM = "Team";
    }

    /// <summary>
    /// 移动结束的回调
    /// </summary>
    public delegate void MoveCloseBack();

    /// <summary>
    /// UI关闭结束的回调
    /// </summary>
    public delegate void UICloseBack();
}