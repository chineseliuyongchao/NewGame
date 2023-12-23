namespace Utils.Constant
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
        /// 游戏初始刻
        /// </summary>
        public const int INIT_QUARTER = 1;

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