using System.Collections.Generic;
using QFramework;

namespace Game.GameBase
{
    public interface IGameModel : IModel
    {
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