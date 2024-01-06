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

        /// <summary>
        /// 输出二维数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        void PrintArray(int[,] array);

        /// <summary>
        /// 根据位置和大小生成每个位置单独的key
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int GenerateKey(Vector2Int pos, Vector2Int length);
    }
}