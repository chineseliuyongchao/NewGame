using Fight.Game.Arms;
using Fight.Utils;
using UnityEngine;

namespace Fight.Game.Trait
{
    /// <summary>
    ///     鼓舞士气特质
    /// </summary>
    public class BoostMoraleTrait : ITrait
    {
        public int Id => Constants.BoostMoraleTrait;

        public void Apply(ObjectArmsModel owner)
        {
            var armData = owner.ArmData;
            if (armData == null) return;

            armData.NowMorale += TraitConstants.BoostMoraleTraitMoraleBonus;
        }

        public void Remove(ObjectArmsModel owner)
        {
            var armData = owner.ArmData;
            if (armData == null) return;

            armData.NowMorale -= TraitConstants.BoostMoraleTraitMoraleBonus;
        }
    }
}