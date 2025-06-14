using Battle.BattleBase;
using Battle.Map;
using Battle.Player;
using QFramework;
using UI;
using UnityEngine;

namespace Battle.Team
{
    /// <summary>
    /// 玩家自身的队伍
    /// </summary>
    public class PlayerTeam : BaseTeam
    {
        protected override void OnInit()
        {
            base.OnInit();
            this.GetModel<IBattleBaseModel>().PlayerTeam = this;
        }

        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(OnLocationSelected).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected virtual void OnLocationSelected(SelectMapLocationEvent e)
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
                Debug.Log("Move to Town:" + e.townId);
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