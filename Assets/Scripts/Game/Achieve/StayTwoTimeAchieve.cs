﻿using Game.GameBase;
using QFramework;

namespace Game.Achieve
{
    public class StayTwoTimeAchieve : BaseAchieve
    {
        public override string Name { get; } = "待够两个时辰";

        public override bool CheckFinish()
        {
            return this.GetModel<IGameModel>().NowTime.Time > 2;
        }
    }
}