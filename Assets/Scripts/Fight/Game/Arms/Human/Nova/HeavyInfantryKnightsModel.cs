using System;
using System.Collections.Generic;
using Game.FightCreate;
using QFramework;

namespace Fight.Game.Arms.Human.Nova
{
    /**
     * 步行骑士团
     */
    [Serializable]
    public class HeavyInfantryKnightsModel : ObjectArmsModel
    {
        protected override void OnInit()
        {
            base.OnInit();
            // ArmData = new ArmData(this.GetModel<IGameMenuModel>().ARMDataTypes[Constants.FootKnightsId], Constants.FootKnightsId);
            // TraitSet.Add(Constants.BoostMoraleTrait);
            // TraitSet.Add(Constants.PackLightTrait);

            //获取所有战场上的军队数据
            List<int> info = new List<int>(this.GetModel<IFightCreateModel>().AllLegions.Keys);
            for (int i = 0; i < info.Count; i++)
            {
                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[info[i]];
            }
        }

        public new HeavyInfantryKnightsModel Clone()
        {
            var model = (HeavyInfantryKnightsModel)base.Clone();
            //todo

            return model;
        }
    }
}