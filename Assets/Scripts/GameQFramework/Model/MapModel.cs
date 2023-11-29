using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;

namespace GameQFramework
{
    public class MapModel : AbstractModel, IMapModel
    {

        private PathfindingMap _map;
        
        protected override void OnInit()
        {
            
        }

        public PathfindingMap Map { get; set; }
        public Vector2 MapSize { get; set; }
    }
}