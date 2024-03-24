using UnityEngine;

namespace GameQFramework
{
    public class MathUtility : IMathUtility
    {
        public int PointClosestToAPointOnALineSegment(int lineSegmentStart, int lineSegmentEnd, int point)
        {
            if (point <= lineSegmentStart)
            {
                return lineSegmentStart;
            }

            if (point >= lineSegmentEnd)
            {
                return lineSegmentEnd;
            }

            return point;
        }

        public int Intersection(Vector2 pos1, Vector2 pos2, int x, int y)
        {
            if (x == -1 && y == -1) // 如果传入的x和y都为-1，说明参数不合法
            {
                return -1; // 返回一个特殊值表示参数不合法
            }

            if (x == -1) // 如果x为-1，说明求与高度为y平行于x轴的直线的交点
            {
                if ((int)pos1.x == (int)pos2.x) // 如果线段垂直于y轴，无法求交点
                {
                    return -1; // 返回一个特殊值表示无法求交点
                }

                float intersectionX = pos1.x + (y - pos1.y) / (pos2.y - pos1.y) * (pos2.x - pos1.x); // 计算线段和x轴的交点
                return Mathf.RoundToInt(intersectionX); // 返回交点的x坐标
            }

            if (y == -1) // 如果y为-1，说明求与宽度为x平行于y轴的直线的交点
            {
                if ((int)pos1.y == (int)pos2.y) // 如果线段水平于x轴，无法求交点
                {
                    return -1; // 返回一个特殊值表示无法求交点
                }

                float intersectionY = pos1.y + (x - pos1.x) / (pos2.x - pos1.x) * (pos2.y - pos1.y); // 计算线段和y轴的交点
                return Mathf.RoundToInt(intersectionY); // 返回交点的y坐标
            }

            return -1; // 如果传入的x和y只能有一个为-1
        }

        public int CrossTheBorder(int num, int max, int min)
        {
            if (max < min)
            {
                return num;
            }

            if (num < min)
            {
                num = min;
            }
            else if (num > max)
            {
                num = max;
            }

            return num;
        }
    }
}