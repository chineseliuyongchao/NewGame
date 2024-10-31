using System;
using Fight.Game.AI;
using Fight.System;
using QFramework;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 电脑操控的军队
    /// </summary>
    public class ComputerLegion : BaseLegion
    {
        private BaseLegionAi _baseLegionAi;

        public override void Init(int id)
        {
            base.Init(id);
            _baseLegionAi = new LegionEasyAi(this);
        }

        public override void StartRound(Action<int> action)
        {
            _baseLegionAi.StartRound();
            base.StartRound(action);
        }

        protected override void UnitStartRound()
        {
            _baseLegionAi.CheckNextBehavior(nowUnitId);
        }

        public override void UnitEndAction()
        {
            if (!this.GetSystem<IFightComputeSystem>().EnoughMovePoint(nowUnitController.unitData.unitId))
            {
                UnitEndRound();
            }
            else
            {
                _baseLegionAi.CheckNextBehavior(nowUnitId);
            }
        }
    }
}