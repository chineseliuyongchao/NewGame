using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// 地图节点
    /// </summary>
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