using QFramework;
using UnityEngine;

namespace Battle.Map
{
    public class MapModel : AbstractModel, IMapModel
    {

        private PathfindingMap _map;
        
        protected override void OnInit()
        {
            
        }

        public PathfindingMap Map
        {
            get => _map;
            set => _map = value;
        }

        public Vector2 MapSize { get; set; }
    }
}