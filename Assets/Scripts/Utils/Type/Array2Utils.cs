using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 封装一个二维数组工具类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Array2Utils<T>
    {
        private T[,] _mGrid;
        private readonly int _mWidth;
        private readonly int _mHeight;

        public int Width => _mWidth;
        public int Height => _mHeight;

        public T this[int xIndex, int yIndex]
        {
            get
            {
                if (xIndex >= 0 && xIndex < _mWidth && yIndex >= 0 && yIndex < _mHeight)
                {
                    return _mGrid[xIndex, yIndex];
                }

                return default;
            }
            set
            {
                if (xIndex >= 0 && xIndex < _mWidth && yIndex >= 0 && yIndex < _mHeight)
                {
                    _mGrid[xIndex, yIndex] = value;
                }
            }
        }

        public Array2Utils(int width, int height)
        {
            _mWidth = width;
            _mHeight = height;
            _mGrid = new T[width, height];
        }

        /// <summary>
        /// 为针对元素的操作添加一层越界判断
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="operate"></param>
        /// <returns></returns>
        public bool HandleElement(Vector2Int pos, Action<T> operate)
        {
            if (pos.x < 0 || pos.x >= _mWidth || pos.y < 0 || pos.y >= _mHeight)
            {
                return false;
            }

            operate(_mGrid[pos.x, pos.y]);
            return true;
        }

        /// <summary>
        /// 判断是否越界
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsWithinBounds(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < _mWidth && pos.y >= 0 && pos.y < _mHeight;
        }

        /// <summary>
        /// 遍历所有元素
        /// </summary>
        /// <param name="operate">元素要执行的方法</param>
        public void ForEach(Action<int, int, T> operate)
        {
            for (var x = 0; x < _mWidth; x++)
            {
                for (var y = 0; y < _mHeight; y++)
                {
                    operate(x, y, _mGrid[x, y]);
                }
            }
        }

        /// <summary>
        /// 遍历一个矩形区域内的所有元素
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="operate">元素要执行的方法</param>
        public void ForEach(RectInt rect, Action<int, int, T> operate)
        {
            if (rect.x < 0 || rect.y < 0 || rect.x + rect.width > _mWidth ||
                rect.y + rect.height > _mHeight)
            {
                return;
            }

            for (var x = rect.x; x < rect.x + rect.width; x++)
            {
                for (var y = rect.y; y < rect.y + rect.height; y++)
                {
                    operate(x, y, _mGrid[x, y]);
                }
            }
        }

        /// <summary>
        /// 遍历一个矩形区域周围一圈的元素
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="operate">元素要执行的方法</param>
        public void ForEachAround(RectInt rect, Action<int, int, T> operate)
        {
            for (int i = rect.x - 1; i <= rect.xMax + 1; i++)
            {
                for (int j = rect.y - 1; j <= rect.yMax + 1; j++)
                {
                    // 检查坐标 (i, j) 是否在原始的 RectInt 的周围一圈
                    if (i < rect.x || i > rect.xMax || j < rect.y || j > rect.yMax)
                    {
                        //检查越界
                        if (i >= 0 && i < _mGrid.GetLength(0) && j >= 0 && j < _mGrid.GetLength(1))
                        {
                            operate(i, j, _mGrid[i, j]);
                        }
                    }
                }
            }
        }
    }
}