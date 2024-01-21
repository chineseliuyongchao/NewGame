using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game.Player
{
    public class PlayerController : BaseGameController
    {
        private GameObject _playerPrefab;
        private GameObject _player;

        protected override void OnInit()
        {
            base.OnInit();
            _playerPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.PLAYER_ARMY);
            _player = Instantiate(_playerPrefab, transform);
            // _player.transform.position = new Vector3(-8.5f, -2);
        }

        protected override void OnListenEvent()
        {
        }
    }
}