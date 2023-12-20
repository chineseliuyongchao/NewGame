using QFramework;

namespace GameQFramework
{
    public interface IGameUtility : IUtility
    {
        /// <summary>
        /// 获取"yyyy-MM-dd-HH-mm-ss格式的当前时间
        /// </summary>
        /// <returns></returns>
        string TimeYToS();
    }
}