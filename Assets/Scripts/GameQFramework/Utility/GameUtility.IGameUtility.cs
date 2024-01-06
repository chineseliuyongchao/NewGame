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
        T[] ParseJson<T>(string jsonString);

        /// <summary>
        /// 将数据解析成列表
        /// </summary>
        /// <param name="jsonString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T[] ParseJsonToList<T>(string jsonString);

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

        /// <summary>
        /// 获取一条线段距离一个点最近的点
        /// </summary>
        /// <param name="lineSegmentStart">线段起点</param>
        /// <param name="lineSegmentEnd">线段终点</param>
        /// <param name="point">目标点</param>
        /// <returns></returns>
        int PointClosestToAPointOnALineSegment(int lineSegmentStart, int lineSegmentEnd, int point);
    }
}