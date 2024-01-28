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
                this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId = e.townId;
                if (e.townId != 0)
                {
                    SetTeamType(TeamType.MOVE_TO_TOWN);
                }
                else
                {
                    SetTeamType(TeamType.MOVE_TO_FIELD);
                }

                SetMoveTarget(GetStartMapPos(), e.selectPos, () =>
                {
                    if (e.townId != 0)
                    {
                        ArriveInTown(e.townId);
                        this.GetModel<IMyPlayerModel>().AccessTown++;
                        SetTeamType(TeamType.HUT_TOWN);
                    }
                    else
                    {
                        SetTeamType(TeamType.HUT_FIELD);
                    }
                });
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void ArriveInTown(int townId)
        {
            UIKit.OpenPanel<UITown>(new UITownData(townId));
        }

        protected override float MoveSpeed()
        {
            return base.MoveSpeed() * 5;
        }
    }
}