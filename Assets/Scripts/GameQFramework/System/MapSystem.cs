using EditorUtils.MapToMesh;
using QFramework;
using SystemTool.Pathfinding;
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

        public Vector2 GetGridToMapPos(Vector2 pos)
        {
            Vector2 vector2 = new Vector2(pos.x, pos.y) * MapConstant.GRID_SIZE / MapConstant.MAP_PIXELS_PER_UNIT;
            vector2 -= this.GetModel<IMapModel>().MapSize / 2;
            return vector2;
        }

        public void InitMapMeshData(TextAsset textAsset)
        {
            MeshDataType[] meshDataTypes =
                this.GetUtility<IGameUtility>().ParseJsonToList<MeshDataType>(textAsset.text);
            PathfindingMap map = new PathfindingMap(MapConstant.MAP_MESH_WIDTH, MapConstant.MAP_MESH_HEIGHT);
            for (int i = 0; i < meshDataTypes.Length; i++)
            {
                MeshDataType meshDataType = meshDataTypes[i];
                PathfindingMapNode pathfindingMapNode = new PathfindingMapNode
                {
                    nodeRect = new RectInt(meshDataType.x, meshDataType.y, meshDataType.width, meshDataType.height),
                    terrainType = TerrainType.CAN_PASS
                };
                map.MapData.ForEach(pathfindingMapNode.nodeRect,
                    (x, y, node) => { map.MapData[x, y] = pathfindingMapNode; });
                map.MapData.ForEach((_, _, node) =>
                {
                    if (map.CheckPass(node))
                    {
                        if (node.aroundNode == null)
                        {
                            node.aroundNode = map.FindAroundNode(node);
                        }
                    }
                });
            }

            this.GetModel<IMapModel>().Map = map;
        }
    }
}