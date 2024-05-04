using System.Collections.Generic;
using Game.Team;
using QFramework;

namespace Game.GameBase
{
    public interface IGameModel : IModel
    {
        /// <summary>
        /// 当前的时间
        /// </summary>
        public GameTime NowTime { get; set; }

        /// <summary>
        /// 玩家队伍的实例（有且仅有一个）
        /// </summary>
        public PlayerTeam PlayerTeam { get; set; }

        /// <summary>
        /// 记录时间是否可以流逝
        /// </summary>
        public bool TimeIsPass { get; set; }

        /// <summary>
        /// 可以暂停时间的弹窗打开的次数
        /// </summary>
        public int OpenStopTimeUITime { get; set; }

        /// <summary>
        /// 多语种文本
        /// </summary>
        public Dictionary<int, LocalizationData> LocalizationData { get; set; }

        /// <summary>
        /// 对话多语种文本
        /// </summary>
        public Dictionary<int, LocalizationData> DialogueLocalizationData { get; set; }

        /// <summary>
        /// 对话提示多语种文本
        /// </summary>
        public Dictionary<int, LocalizationData> DialogueTipLocalizationData { get; set; }
    }
}