using System;

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
        public bool HandleElement(IntVector2 pos, Action<T> operate)
        {
            if (pos.X < 0 || pos.X >= _mHeight || pos.Y < 0 || pos.Y >= _mWidth)
            {
                return false;
            }

            operate(_mGrid[pos.X, pos.Y]);
            return true;
        }

        /// <summary>
        /// 判断是否越界
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsWithinBounds(IntVector2 pos)
        {
            return pos.X >= 0 && pos.X < _mHeight && pos.Y >= 0 && pos.Y < _mWidth;
        }
    }
}