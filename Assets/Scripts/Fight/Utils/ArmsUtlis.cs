using System;
using UnityEngine;
using Random = System.Random;

namespace Fight.Utils
{
    public static class ArmsUtils
    {
        private static readonly Random Random;

        static ArmsUtils()
        {
            var seed = Mathf.FloorToInt(DateTime.Now.Ticks % int.MaxValue);
            Random = new Random(seed);
        }

        public static int GetRandomByTime()
        {
            return Random.Next();
        }
    }
}