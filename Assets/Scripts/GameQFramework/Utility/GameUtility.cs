using System;

namespace GameQFramework
{
    public class GameUtility : IGameUtility
    {
        public string TimeYToS()
        {
            DateTime currentTime = DateTime.Now;
            // 获取年-月-日-小时-分-秒
            string formattedDateTime = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");
            return formattedDateTime;
        }
    }
}