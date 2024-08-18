using Battle.BattleBase;
using QFramework;

namespace Game.Achieve
{
    public class StayTwoTimeAchieve : BaseAchieve
    {
        public override string Name { get; } = "待够两个时辰";

        public override bool CheckFinish()
        {
            //成就系统改成进游戏就打开，但是没有进入战役没有实例化时间
            if (this.GetModel<IBattleBaseModel>().NowTime != null)
            {
                return this.GetModel<IBattleBaseModel>().NowTime.Time > 2;
            }

            return false;
        }
    }
}