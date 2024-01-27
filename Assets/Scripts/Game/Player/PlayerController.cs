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
        private GameObject _armyPrefab;
        private GameObject _player;

        protected override void OnInit()
        {
            base.OnInit();
            _playerPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.PLAYER_ARMY);
            _player = Instantiate(_playerPrefab, transform);
            // _player.transform.position = new Vector3(-8.5f, -2);

            _armyPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.ARMY);
            List<int> armyDataKey = new List<int>(this.GetModel<IArmyModel>().ArmyData.Keys);
            for (int i = 0; i < armyDataKey.Count; i++)
            {
                ArmyData armyData = this.GetModel<IArmyModel>().ArmyData[armyDataKey[i]];
                if (armyData.generalRoleId == this.GetModel<IMyPlayerModel>().RoleId)
                {
                    continue;
                }

                GameObject armyObject = Instantiate(_armyPrefab, transform);
                Army.Army army = armyObject.GetComponent<Army.Army>();
                army.ArmyId = armyDataKey[i];
            }
        }

        protected override void OnListenEvent()
        {
        }
    }
}