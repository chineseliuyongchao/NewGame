using QFramework;
using UnityEngine;
using Utils.Constant;

namespace GameQFramework
{
    public class MapSystem : AbstractSystem, IMapSystem
    {
        private SpriteRenderer _map;

        protected override void OnInit()
        {
        }

        public void SetMapGameObject(SpriteRenderer map)
        {
            _map = map;
        }

        public Vector2 GetMapPos(Transform transform)
        {
            Vector3 peopleWorldPosition = transform.position;
            Vector3 peopleRelativePosition = _map.transform.InverseTransformPoint(peopleWorldPosition);
            return peopleRelativePosition;
        }

        public Vector2 GetMapToRealPos(Transform transform, Vector2 mapPos)
        {
            Vector3 peopleRelativePosition = transform.InverseTransformPoint(mapPos);
            return peopleRelativePosition;
        }

        public Vector2Int GetGridMapPos(Vector2 pos)
        {
            pos += this.GetModel<IMapModel>().MapSize / 2;
            Vector2 vector2 = pos * MapConstant.MAP_PIXELS_PER_UNIT / MapConstant.GRID_SIZE;
            Vector2Int res = new Vector2Int((int)vector2.x, (int)vector2.y);
            return res;
        }

        public Vector2 GetGridToMapPos(Vector2Int pos)
        {
            Vector2 vector2 = new Vector2(pos.x, pos.y) * MapConstant.GRID_SIZE / 100;
            vector2 -= this.GetModel<IMapModel>().MapSize / 2;
            return vector2;
        }
    }
}