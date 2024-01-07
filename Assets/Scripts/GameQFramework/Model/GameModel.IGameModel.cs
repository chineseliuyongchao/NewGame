using QFramework;

namespace GameQFramework
{
    public interface IGameModel : IModel
    {
        /// <summary>
        /// 记录年，默认从一年开始
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 记录月，默认从一月开始
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 记录日，默认从一天开始
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 记录时辰，默认从子时开始
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 记录刻，默认从一刻开始
        /// </summary>
        public int Quarter { get; set; }

        /// <summary>
        /// 记录时间是否可以流逝
        /// </summary>
        public bool TimeIsPass { get; set; }
    }
}