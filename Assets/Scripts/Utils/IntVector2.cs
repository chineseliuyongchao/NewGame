using System;

namespace Utils
{
    /// <summary>
    /// 使用int的Vector2
    /// </summary>
    public class IntVector2
    {
        public readonly int X;
        public readonly int Y;

        public IntVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return "x:  "+X+"  y:  "+Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is IntVector2)
            {
                return ((IntVector2)obj).X==X&&((IntVector2)obj).Y==Y;
            }

            return false;
        }

        protected bool Equals(IntVector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}