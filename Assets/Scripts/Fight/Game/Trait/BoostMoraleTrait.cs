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
            var attributes = owner.SpiritualAttribute;
            if (attributes == null) return;

            attributes.currentFightWill =
                Mathf.Clamp(attributes.currentFightWill + TraitConstants.BoostMoraleTraitMoraleBonus, 0,
                    attributes.totalFightWill);
        }

        public void Remove(ObjectArmsModel owner)
        {
            var attributes = owner.SpiritualAttribute;
            if (attributes == null) return;

            attributes.currentFightWill = Mathf.Clamp(attributes.currentFightWill - 8, 0, attributes.totalFightWill);
        }
    }
}