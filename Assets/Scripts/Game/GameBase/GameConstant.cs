namespace Game.GameBase
{
    /// <summary>
    /// 游戏相关常量
    /// </summary>
    public abstract class GameConstant
    {
        /// <summary>
        /// 存档文件夹
        /// </summary>
        public const string SAVE_FOLDER_NAME = "/GameSave";

        /// <summary>
        /// 保存游戏数据的文件夹
        /// </summary>
        public const string SAVE_GAME_DATA_FOLDER_NAME = "/GameDataSave";

        /// <summary>
        /// 保存游戏存档数据的文件
        /// </summary>
        public const string GAME_FILE_DATA = "/gameFileData.txt";

        /// <summary>
        /// 队伍基础移动速度
        /// </summary>
        public const int BASE_MOVE_SPEED = 1;
    }

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
    /// 配置表相关常量
    /// </summary>
    public abstract class ConfigurationTableConstant
    {
        /// <summary>
        /// 聚落配置表
        /// </summary>
        public const string TOWN_INFORMATION = "TownInformation";

        /// <summary>
        /// 聚落名字
        /// </summary>
        public const string TOWN_NAME = "TownName";

        /// <summary>
        /// 家族配置表
        /// </summary>
        public const string FAMILY_INFORMATION = "FamilyInformation";

        /// <summary>
        /// 家族名字
        /// </summary>
        public const string FAMILY_NAME = "FamilyName";

        /// <summary>
        /// 角色配置表
        /// </summary>
        public const string ROLE_INFORMATION = "RoleInformation";

        /// <summary>
        /// 角色名字
        /// </summary>
        public const string ROLE_NAME = "RoleName";

        /// <summary>
        /// 国家配置表
        /// </summary>
        public const string COUNTRY_INFORMATION = "CountryInformation";

        /// <summary>
        /// 家族名字
        /// </summary>
        public const string COUNTRY_NAME = "CountryName";
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
    /// 地图数据配置表相关常量
    /// </summary>
    public abstract class MapConfigurationTableConstant
    {
        /// <summary>
        /// 地图网格数据表
        /// </summary>
        public const string MAP_MESH_INFORMATION = "MapMeshInformation";
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