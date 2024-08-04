using System;
using Fight.Utils;

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
            ObjectAttribute.totalBlood = 6000;
            ObjectAttribute.currentBlood = 6000;
            ObjectAttribute.totalPeople = 200;
            ObjectAttribute.currentPeople = 200;
            ObjectAttribute.attackPower = 50;
            ObjectAttribute.chargePower = 50;
            ObjectAttribute.normalDamage = 50;
            ObjectAttribute.armorBreakDamage = 50;
            ObjectAttribute.armorStrength = 100;
            ObjectAttribute.movePower = 10;
            ObjectAttribute.viewPower = 10;
            ObjectAttribute.hiddenPower = 5;
            ObjectAttribute.attackRange = 1;

            SpiritualAttribute.totalFightWill = 80;
            SpiritualAttribute.currentFightWill = 80;
            SpiritualAttribute.totalFatigue = 100;
            SpiritualAttribute.currentFatigue = 0;

            TraitSet.Add(Constants.BoostMoraleTrait);
            TraitSet.Add(Constants.PackLightTrait);
        }

        public new HeavyInfantryKnightsModel Clone()
        {
            var model = (HeavyInfantryKnightsModel)base.Clone();
            //todo

            return model;
        }
    }
}