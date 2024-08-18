using QFramework;

namespace Battle.Player
{
    public interface IMyPlayerModel : IModel
    {
        /// <summary>
        /// 主角拜访过的聚落数量
        /// </summary>
        public int AccessTown { get; set; }

        /// <summary>
        /// 用于保存创建游戏时产生的数据
        /// </summary>
        public CreateGameData CreateGameData { get; set; }

        /// <summary>
        /// 玩家的人物id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 玩家的家族id，开始游戏以后，玩家默认创建一个家族
        /// </summary>
        public int FamilyId { get; set; }

        /// <summary>
        /// 玩家的队伍id，开始游戏以后，玩家默认都要创建一只队伍，即便只有一个人
        /// </summary>
        public int TeamId { get; set; }
    }
}