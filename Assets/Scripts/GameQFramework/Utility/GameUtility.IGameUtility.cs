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

        /// <summary>
        /// 将数字转变成xxx,xxx,xxxK/M/B/T的形式
        /// </summary>
        /// <param name="num">传入的数字</param>
        /// <param name="overPower">要保留几位数字（不包括逗号），剩余的的使用KMBT代表</param>
        /// <returns></returns>
        string NumToKmbt(long num, int overPower);
    }
}