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

        /// <summary>
        /// 多语种文本配置表
        /// </summary>
        public const string LOCALIZATION_TEXT = "LocalizationText";

        /// <summary>
        /// 对话多语种文本配置表
        /// </summary>
        public const string DIALOGUE_LOCALIZATION_TEXT = "DialogueLocalizationText";

        /// <summary>
        /// 对话提示多语种文本配置表
        /// </summary>
        public const string DIALOGUE_TIP_LOCALIZATION_TEXT = "DialogueTipLocalizationText";

        /// <summary>
        /// 兵种数据表
        /// </summary>
        public const string TROOPS_NUMBER = "troopsNumber";

        /// <summary>
        /// 派系信息表
        /// </summary>
        public const string FACTION_INFORMATION = "FactionInformation";
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

    public enum LocalizationType
    {
        /// <summary>
        /// 普通文本
        /// </summary>
        NORMAL,

        /// <summary>
        /// 对话
        /// </summary>
        DIALOGUE,

        /// <summary>
        /// 对话提示
        /// </summary>
        DIALOGUE_TIP
    }
}