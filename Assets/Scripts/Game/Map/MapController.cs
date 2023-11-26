using GameQFramework;

namespace Game.Map
{
    public class MapController : BaseGameController
    {
        public MapNode mapNode;

        protected override void OnInit()
        {
            Instantiate(mapNode, transform);
        }
    }
}