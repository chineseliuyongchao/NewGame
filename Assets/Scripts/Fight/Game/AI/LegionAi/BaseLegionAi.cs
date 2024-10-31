using System.Collections.Generic;
using Fight.Game.Legion;
using Game.FightCreate;
using Game.GameBase;
using QFramework;

namespace Fight.Game.AI
{
    /// <summary>
    /// 军队ai的基类，每个电脑军队只有一个军队ai，可以看成电脑军队的指挥部
    /// </summary>
    public abstract class BaseLegionAi : IController
    {
        public readonly ComputerLegion computerLegion;
        protected LegionStartRoundAi legionStartRoundAi;

        /// <summary>
        /// 存放每个单位要执行的行为（单位id，单位行为）
        /// </summary>
        public readonly Dictionary<int, UnitBehaviorAi> unitBehaviorAis;

        protected BaseLegionAi(ComputerLegion computerLegion)
        {
            this.computerLegion = computerLegion;
            unitBehaviorAis = new Dictionary<int, UnitBehaviorAi>();
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[computerLegion.legionId];
            List<int> unitId = new List<int>(legionInfo.allUnit.Keys);
            for (int i = 0; i < unitId.Count; i++)
            {
                unitBehaviorAis.Add(unitId[i], new UnitBehaviorAi());
            }
        }

        /// <summary>
        /// 军队开始回合
        /// </summary>
        public abstract void StartRound();

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        /// <summary>
        /// 检查ai的下一个行为
        /// </summary>
        public void CheckNextBehavior(int unitId)
        {
            UnitBehaviorAi unitBehaviorAi = unitBehaviorAis[unitId];
            if (unitBehaviorAi.unitBehaviorAiSingles.Count <= 0)
            {
                computerLegion.UnitEndRound();
                return;
            }

            BaseUnitBehaviorAiSingle aiSingle = unitBehaviorAi.unitBehaviorAiSingles[0];
            aiSingle.StartBehavior(isEnd =>
            {
                if (isEnd)
                {
                    unitBehaviorAi.unitBehaviorAiSingles.Remove(aiSingle);
                }

                computerLegion.UnitEndAction();
            });
        }
    }

    /// <summary>
    /// 记录一个单位的行为
    /// </summary>
    public class UnitBehaviorAi
    {
        public List<BaseUnitBehaviorAiSingle> unitBehaviorAiSingles = new();
    }
}