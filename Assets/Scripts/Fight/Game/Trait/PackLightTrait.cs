using Fight.Game.Arms;
using Fight.Utils;

namespace Fight.Game.Trait
{
    /// <summary>
    ///     轻装特质
    /// </summary>
    public class PackLightTrait : ITrait
    {
        public int Id => Constants.PackLightTrait;

        public void Apply(ObjectArmsModel attributes)
        {
        }

        public void Remove(ObjectArmsModel attributes)
        {
        }
    }
}