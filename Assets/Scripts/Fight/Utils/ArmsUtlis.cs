using System;
using Fight.Commands;
using Fight.Enum;
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

        public static Vector3 GetArmsRelayPosition(this ArmsController controller)
        {
            return (Vector3)FightScene.Ins.aStarModel.FightGridNodeInfoList[controller.fightCurrentIndex].position;
        }
    }
}