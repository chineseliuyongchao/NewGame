using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IMathUtility : IUtility
    {
        /// <summary>
        /// 获取一条线段距离一个点最近的点
        /// </summary>
        /// <param name="lineSegmentStart">线段起点</param>
        /// <param name="lineSegmentEnd">线段终点</param>
        /// <param name="point">目标点</param>
        /// <returns></returns>
        int PointClosestToAPointOnALineSegment(int lineSegmentStart, int lineSegmentEnd, int point);

        /// <summary>
        /// 获取一条直线对于平行于x轴或者y轴的交点（x和y一个传值，另一个传-1，用来确定和哪个轴求交点，如果x是-1，那么就是求和高度为y平行于x轴的直线的交点）
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int Intersection(Vector2 pos1, Vector2 pos2, int x, int y);

        /// <summary>
        /// 越界规避
        /// </summary>
        /// <param name="num"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        int CrossTheBorder(int num, int max, int min);
    }
}