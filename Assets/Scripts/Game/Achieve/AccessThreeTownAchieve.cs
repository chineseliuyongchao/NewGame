using Game.Player;
using QFramework;

namespace Game.Achieve
{
    public class AccessThreeTownAchieve : BaseAchieve
    {
        public override string Name { get; } = "访问三个聚落";

        public override bool CheckFinish()
        {
            return this.GetModel<IMyPlayerModel>().AccessTown >= 3;
        }
    }
}