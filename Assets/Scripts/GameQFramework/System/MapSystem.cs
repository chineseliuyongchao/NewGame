using Game.Map;
using QFramework;
using UnityEngine;
using Utils;

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

        public IntVector2 GetGridMapPos(Vector2 pos)
        {
            pos += this.GetModel<IMapModel>().MapSize / 2;
            Vector2 vector2 = pos * MapConstant.MAP_PIXELS_PER_UNIT / MapConstant.GRID_SIZE;
            IntVector2 res = new IntVector2((int)vector2.x, (int)vector2.y);
            return res;
        }

        public Vector2 GetGridToMapPos(IntVector2 pos)
        {
            Vector2 vector2 = new Vector2(pos.X, pos.Y) * MapConstant.GRID_SIZE / 100;
            vector2 -= this.GetModel<IMapModel>().MapSize / 2;
            float halfGridRealSize = MapConstant.GRID_SIZE / 2f / MapConstant.MAP_PIXELS_PER_UNIT;
            vector2 += new Vector2(halfGridRealSize, halfGridRealSize); //返回的位置应该是寻路方格的中心
            return vector2;
        }
    }
}