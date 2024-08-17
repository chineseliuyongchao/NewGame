using System;
using Fight.Model;
using Fight.Utils;
using Game.GameMenu;
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
            ArmData = new ArmData(this.GetModel<IGameMenuModel>().ARMDataTypes[Constants.FootKnightsId],
                Constants.FootKnightsId);
            // TraitSet.Add(Constants.BoostMoraleTrait);
            // TraitSet.Add(Constants.PackLightTrait);
        }

        public new HeavyInfantryKnightsModel Clone()
        {
            var model = (HeavyInfantryKnightsModel)base.Clone();
            //todo

            return model;
        }
    }
}