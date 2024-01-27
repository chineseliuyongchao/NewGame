using GameQFramework;
using QFramework;
using UI;

namespace Game.Team
{
    /// <summary>
    /// 玩家自身的队伍
    /// </summary>
    public class PlayerTeam : BaseTeam
    {
        protected override void OnInit()
        {
            base.OnInit();
            this.GetModel<IGameModel>().PlayerTeam = this;
        }

        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                SetMoveTarget(GetStartMapPos(), e.selectPos, () =>
                {
                    if (e.townId != 0)
                    {
                        ArriveInTown(e.townId);
                        this.GetModel<IMyPlayerModel>().AccessTown++;
                    }
                });
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void ArriveInTown(int townId)
        {
            UIKit.OpenPanel<UITown>(new UITownData(townId));
        }
    }
}