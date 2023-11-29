using GameQFramework;
using QFramework;
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
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                _people.transform.position =
                    this.GetSystem<IMapSystem>().GetMapToRealPos(_people.transform.parent, e.SelectPos);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}