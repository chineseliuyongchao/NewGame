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
            TeamId = this.GetModel<IMyPlayerModel>().TeamId;
            this.GetModel<IGameModel>().PlayerTeam = this;
        }

        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                if (e.townId != 0)
                {
                    this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.MOVE_TO_TOWN;
                }
                else
                {
                    this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.MOVE_TO_FIELD;
                }

                SetMoveTarget(GetStartMapPos(), e.selectPos, () =>
                {
                    if (e.townId != 0)
                    {
                        ArriveInTown(e.townId);
                        this.GetModel<IMyPlayerModel>().AccessTown++;
                        this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.HUT_TOWN;
                    }
                    else
                    {
                        this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.HUT_FIELD;
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