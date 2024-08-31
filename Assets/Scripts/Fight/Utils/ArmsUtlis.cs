using System;
using Fight.Game.Arms;
using Fight.Scenes;
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

        public static Vector3 GetArmsRelayPosition(this ArmsController controller)
        {
            return (Vector3)FightScene.Ins.aStarModel.fightGridNodeInfoList[controller.armData.currentPosition].position;
        }
    }
}