﻿namespace Fight.Utils
{
    public class Constants
    {
        /**
         * 鼠标的相关设置
         */
        public const float ZoomSpeed = 1.5f; // 缩放速度

        public const float MinZoom = 1f; // 最小缩放值
        public const float MaxZoom = 5f; // 最大缩放值

        /**
         * 拖动的相关设置
         */
        public const float DragSpeed = 1.5f; // 拖动速度

        public const float WorldHeight = 10f; // 世界高度
        public const float WorldWidth = WorldHeight / 0.5625f; // 世界宽度

        #region 兵种名

        public const string HeavyInfantryKnights = "HeavyInfantryKnights";

        #endregion

        /**
         * 玩家自定义兵种的布阵所用的行列最大值
         */
        public const int ArmsRow = 3;

        public const int ArmsCol = 6;

        /**
         * 战斗界面的世界宽高
         */
        public const float FightSceneWorldHeight = 10;

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

        public const int FightNodeWidthNum = 20;
        public const int FightNodeHeightNum = 16;

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
        public const int Head = 0;

        /// <summary>
        ///     身体
        /// </summary>
        public const int Body = 1;

        /// <summary>
        ///     头盔
        /// </summary>
        public const int Helmet = 2;

        /// <summary>
        ///     盔甲
        /// </summary>
        public const int Armor = 3;

        /// <summary>
        ///     武器
        /// </summary>
        public const int Weapon = 4;

        /// <summary>
        ///     盾牌
        /// </summary>
        public const int Shield = 5;

        /// <summary>
        ///     腿
        /// </summary>
        public const int Foot = 6;

        #endregion
    }
}