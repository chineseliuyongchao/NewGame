namespace Game.Game
{
    /// <summary>
    /// 游戏相关常量
    /// </summary>
    public abstract class GameConstant
    {
        /// <summary>
        /// 现实秒换算游戏刻
        /// </summary>
        public const float CONVERT_QUARTER = 1.5f;

        /// <summary>
        /// 刻换算时辰
        /// </summary>
        public const int CONVERT_TIME = 8;

        /// <summary>
        /// 时辰换算日
        /// </summary>
        public const int CONVERT_DAY = 12;

        /// <summary>
        /// 日换算月
        /// </summary>
        public const int CONVERT_MONTH = 30;

        /// <summary>
        /// 月换算年
        /// </summary>
        public const int CONVERT_YEAR = 12;

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
    }
}