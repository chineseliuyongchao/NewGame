using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.Map
{
    public class MapNode : BaseGameController
    {
        public SpriteRenderer map;

        protected override void OnInit()
        {
            base.OnInit();
            this.GetModel<IMapModel>().MapSize = map.bounds.size;
            this.GetSystem<IMapSystem>().SetMapGameObject(map);
        }
    }
}