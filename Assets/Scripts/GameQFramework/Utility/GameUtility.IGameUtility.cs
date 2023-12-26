using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Utils;

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

        /// <summary>
        /// 把json中的数据解析到字典中
        /// </summary>
        /// <param name="textAsset"></param>
        /// <param name="dictionary"></param>
        /// <typeparam name="T"></typeparam>
        void AnalysisJsonConfigurationTable<T>(TextAsset textAsset, Dictionary<int, T> dictionary)
            where T : BaseJsonData;

        /// <summary>
        /// 将JSON数据转换成指定类型的对象数组
        /// </summary>
        /// <param name="jsonString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ParseJson<T>(string jsonString) where T : BaseJsonData;
    }
}