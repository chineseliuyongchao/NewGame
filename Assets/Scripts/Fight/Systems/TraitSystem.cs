using System.Collections.Generic;
using Fight.Game.Trait;
using Fight.Utils;
using QFramework;

namespace Fight.Systems
{
    public class TraitSystem : AbstractSystem
    {
        private readonly Dictionary<int, ITrait> _traitDictionary = new();

        protected override void OnInit()
        {
            _traitDictionary[Constants.BoostMoraleTrait] = new BoostMoraleTrait();
            _traitDictionary[Constants.PackLightTrait] = new PackLightTrait();
        }

        public ITrait GetTraitById(int id)
        {
            _traitDictionary.TryGetValue(id, out var trait);
            return trait;
        }
    }
}