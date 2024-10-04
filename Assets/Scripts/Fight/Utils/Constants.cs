namespace Fight.Utils
{
    public class Constants
    {
        #region 兵种id

        public const int ElfRecruit = 0;

        public const int FootKnightsId = 100;
        public const int SquireFootKnights = 101;
        public const int TheKnights = 104;
        public const int KnightsOfTheSquire = 105;

        #endregion

        #region 兵种名

        public const string HeavyInfantryKnights = "HeavyInfantryKnights";

        #endregion

        /**
         * 玩家自定义兵种的布阵所用的行列最大值
         */
        public const int UnitRow = 3;

        public const int UnitCol = 6;

        /**
         * 战斗界面的世界宽高
         */
        public const float FightSceneWorldHeight = 20;

        public const float FightSceneWorldWidth = FightSceneWorldHeight / 0.5625f;

        public const float FightSceneWorldHeightHalf = FightSceneWorldHeight / 2f;
        public const float FightSceneWorldWidthHalf = FightSceneWorldWidth / 2f;

        /**
         * 战斗界面的格子宽高
         */
        public const float FightNodeWidth = 1.665f;

        public const float FightNodeHeight = FightNodeWidth * 0.64f;

        public const float FightNodeWidthHalf = FightNodeWidth / 2f;
        public const float FightNodeHeightHalf = FightNodeHeight / 2f;

        public const float FightNodeWidthOffset = 0.1f;
        public const float FightNodeHeightOffset = 0.1f;

        public const int FightNodeWidthNum = 39;
        public const int FightNodeHeightNum = 31;

        public const int FightNodeVisibleWidthNum = 31;
        public const int FightNodeVisibleHeightNum = 24;

        #region 特质id

        /**
         * 剑盾
         */
        public const int PackLightTrait = 1000;

        /**
         * 鼓舞士气
         */
        public const int BoostMoraleTrait = 2000;

        #endregion

        #region 兵种可视化界面的对应部位id

        /// <summary>
        ///     头
        /// </summary>
        public const int Head = 2;

        /// <summary>
        ///     身体
        /// </summary>
        public const int Body = 1;

        /// <summary>
        ///     头盔
        /// </summary>
        public const int Helmet = 4;

        /// <summary>
        ///     盔甲
        /// </summary>
        public const int Armor = 3;

        /// <summary>
        ///     武器
        /// </summary>
        public const int Weapon = 6;

        /// <summary>
        ///     盾牌
        /// </summary>
        public const int Shield = 5;

        /// <summary>
        ///     腿
        /// </summary>
        public const int Foot = 0;

        /// <summary>
        /// 用于调试的文本展示
        /// </summary>
        public const int DebugText = 7;

        #endregion

        /// <summary>
        /// todo
        /// 我军的战场布局信息（随机）
        /// </summary>
        public static readonly int[] MyUnitPositionArray1 =
        {
            147, 131, 115, 99, 83, 67, 35,
            164, 148, 132, 116, 100, 84, 68, 52, 36,
            149, 133, 117, 101, 85, 69, 53, 37,
            166, 150, 134, 118, 102, 86, 70, 54, 38
        };

        /// <summary>
        /// todo
        /// 敌军的布局信息（随机）
        /// </summary>
        public static readonly int[] MyUnitPositionArray2 =
        {
            147 + 5, 131 + 5, 115 + 5, 99 + 5, 83 + 5, 67 + 5, 35 + 5,
            164 + 5, 148 + 5, 132 + 5, 116 + 5, 100 + 5, 84 + 5, 68 + 5, 52 + 5, 36 + 5,
            149 + 5, 133 + 5, 117 + 5, 101 + 5, 85 + 5, 69 + 5, 53 + 5, 37 + 5,
            166 + 5, 150 + 5, 134 + 5, 118 + 5, 102 + 5, 86 + 5, 70 + 5, 54 + 5, 38 + 5
        };

        /// <summary>
        /// 阵营1的id，通常是玩家所在阵营
        /// </summary>
        public const int BELLIGERENT1 = 1;

        /// <summary>
        /// 阵营2的id，通常是敌人所在的阵营
        /// </summary>
        public const int BELLIGERENT2 = 2;

        /// <summary>
        /// 临时变量，系统为玩家生成的军队id就是这个值
        /// </summary>
        public const int PlayLegionId = 1001;

        /// <summary>
        /// 兵种悬停展示气泡的时间
        /// </summary>
        public const float HoverThreshold = 1f;

        /// <summary>
        /// 初始移动点数
        /// </summary>
        public const int InitMovementPoints = 50;

        /// <summary>
        /// 用于计算移动点数消耗的参数
        /// </summary>
        public const int MovementParameter = 20;
    }
}