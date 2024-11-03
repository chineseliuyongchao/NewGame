using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using Game.FightCreate;
using QFramework;

namespace Fight.Game.AI
{
    public class LegionStartRoundEasyAi : LegionStartRoundAi
    {
        public LegionStartRoundEasyAi(BaseLegionAi baseLegionAi) : base(baseLegionAi)
        {
        }

        public override void Operation()
        {
            //最简单的ai行为就是让每个单位寻找最近的敌方单位靠近并且攻击
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[baseLegionAi.computerLegion.legionId];
            List<int> unitId = new List<int>(legionInfo.allUnit.Keys);
            for (int i = 0; i < unitId.Count; i++)
            {
                UnitController unitController = ProximateEnemy(unitId[i]);
                UnitBehaviorAi behaviorAi = baseLegionAi.unitBehaviorAis[unitId[i]];
                behaviorAi.unitBehaviorAiSingles.Clear();
                behaviorAi.unitBehaviorAiSingles.Add(new UnitAiMoveInAttackRange
                {
                    unitId = unitId[i],
                    behaviorType = AiBehaviorType.MOVE_IN_ATTACK_RANGE,
                    targetUnitId = unitController.unitData.unitId,
                    mustFinish = false
                });
                behaviorAi.unitBehaviorAiSingles.Add(new UnitAiAttack()
                {
                    unitId = unitId[i],
                    behaviorType = AiBehaviorType.MOVE_IN_ATTACK_RANGE,
                    targetUnitId = unitController.unitData.unitId,
                    mustFinish = true,
                    fightAttackType = FightAttackType.SUSTAIN_ADVANCE
                });
            }
        }
    }
}