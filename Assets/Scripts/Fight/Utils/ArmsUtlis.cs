using System;
using Fight.Commands;
using Fight.Enum;
using Fight.Game.Arms;
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

        public static ObjectArmsModel[] GetArmsArrayByRow(this ObjectArmsModel owner, int row = 1)
        {
            return null;
        }

        public static TraitCommand GetTraitCommandByArms(this ObjectArmsModel owner,
            TraitActionType actionType, int traitId)
        {
            return new TraitCommand(owner, actionType, traitId);
        }

        public static int GetRandomByTime()
        {
            return Random.Next();
        }
    }
}