using System.Collections.Generic;
using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game.Player
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
            // _player.transform.position = new Vector3(-8.5f, -2);

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
                Team.Team team = teamObject.GetComponent<Team.Team>();
                team.TeamId = teamDataKey[i];
            }
        }

        protected override void OnListenEvent()
        {
        }
    }
}