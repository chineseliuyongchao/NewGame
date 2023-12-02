using GameQFramework;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : BaseGameController
    {
        public GameObject character;
        private GameObject _people;

        protected override void OnInit()
        {
            base.OnInit();
            _people = Instantiate(character, this.transform);
        }

        protected override void OnListenEvent()
        {
        }
    }
}