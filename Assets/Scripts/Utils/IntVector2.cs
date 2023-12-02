using System;
using System.Runtime.CompilerServices;

namespace Utils
{
    /// <summary>
    /// 使用int的Vector2
    /// </summary>
    public struct IntVector2
    {
        public bool Equals(IntVector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is IntVector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public int X;
        public int Y;

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "X:  " + X + "  Y:  " + Y;
        }

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator +(IntVector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator -(IntVector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator *(IntVector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator /(IntVector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator -(IntVector2 a) => new(-a.X, -a.Y);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator *(IntVector2 a, int d) => new(a.X * d, a.Y * d);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator *(int d, IntVector2 a) => new(a.X * d, a.Y * d);

        [MethodImpl((MethodImplOptions)256)]
        public static IntVector2 operator /(IntVector2 a, int d) => new(a.X / d, a.Y / d);

        [MethodImpl((MethodImplOptions)256)]
        public static bool operator ==(IntVector2 lhs, IntVector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        [MethodImpl((MethodImplOptions)256)]
        public static bool operator !=(IntVector2 lhs, IntVector2 rhs) => !(lhs == rhs);
    }
}