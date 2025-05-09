﻿using System;
using Fight.Game.AI;
using Game.FightCreate;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 电脑操控的军队
    /// </summary>
    public class ComputerLegion : BaseLegion
    {
        private BaseLegionAi _baseLegionAi;

        public override void Init(LegionInfo info)
        {
            base.Init(info);
            _baseLegionAi = new LegionEasyAi(this);
        }

        public override void StartRound(Action<int> action)
        {
            _baseLegionAi.StartRound();
            base.StartRound(action);
        }

        protected override void UnitStartRound()
        {
            if (!_baseLegionAi.CheckNextBehavior(nowUnitId))
            {
                UnitEndRound();
            }
        }

        public override void UnitEndAction()
        {
            if (!_baseLegionAi.CheckNextBehavior(nowUnitId))
            {
                UnitEndRound();
            }
        }
    }
}