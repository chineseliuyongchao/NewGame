using System.Collections.Generic;
using Battle.BattleBase;
using Battle.Team;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Battle.Player
{
    public class PlayerController : BaseGameController
    {
        private GameObject _playerPrefab;
        private GameObject _teamPrefab;
        private GameObject _player;

        protected override void OnInit()
        {
            base.OnInit();
            _playerPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.PLAYER_TEAM);
            _player = Instantiate(_playerPrefab, transform);
            int playerTeamId = this.GetModel<IMyPlayerModel>().TeamId;
            int familyId = this.GetModel<IMyPlayerModel>().FamilyId;
            _player.transform.position = this.GetModel<ITeamModel>().TeamData[playerTeamId].pos;
            PlayerTeam playerTeam = _player.GetComponent<PlayerTeam>();
            playerTeam.InitTeam(playerTeamId, familyId);

            _teamPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.TEAM);
            List<int> teamDataKey = new List<int>(this.GetModel<ITeamModel>().TeamData.Keys);
            for (int i = 0; i < teamDataKey.Count; i++)
            {
                TeamData teamData = this.GetModel<ITeamModel>().TeamData[teamDataKey[i]];
                if (teamData.generalRoleId == this.GetModel<IMyPlayerModel>().RoleId)
                {
                    continue;
                }

                GameObject teamObject = Instantiate(_teamPrefab, transform);
                teamObject.transform.position = teamData.pos;
                Team.Team team = teamObject.GetComponent<Team.Team>();
                team.InitTeam(teamDataKey[i], familyId);
            }
        }

        protected override void OnListenEvent()
        {
        }
    }
}