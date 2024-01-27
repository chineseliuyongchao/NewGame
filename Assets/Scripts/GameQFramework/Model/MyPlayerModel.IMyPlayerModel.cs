using QFramework;

namespace GameQFramework
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
    }
}