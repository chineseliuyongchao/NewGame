using QFramework;

namespace GameQFramework
{
    public interface IMyPlayerModel : IModel
    {
        /// <summary>
        /// 主角拜访过的城市数量
        /// </summary>
        public int AccessTown { get; set; }
    }
}