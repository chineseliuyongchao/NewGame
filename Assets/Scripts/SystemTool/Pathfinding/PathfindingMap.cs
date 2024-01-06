using System.Collections.Generic;
using GameQFramework;
using QFramework;
using UnityEngine;
using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统的地图，记录所有节点
    /// </summary>
    public class PathfindingMap : ICanGetUtility
    {
        private Array2Utils<PathfindingMapNode> _mapData;

        public Array2Utils<PathfindingMapNode> MapData
        {
            get => _mapData;
            set => _mapData = value;
        }

        public PathfindingMap(int width, int height)
        {
            _mapData = new Array2Utils<PathfindingMapNode>(width, height);
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public bool IsWithinBounds(Vector2Int pos)
        {
            return _mapData.IsWithinBounds(pos);
        }

        /// <summary>
        /// 获取一个节点的周围八个节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Dictionary<int, PathfindingMapNode> FindAroundNode(PathfindingMapNode node)
        {
            Dictionary<int, PathfindingMapNode> surroundingElements = new Dictionary<int, PathfindingMapNode>();
            _mapData.ForEachAround(new RectInt
            {
                x = node.pos.x,
                y = node.pos.y,
                width = node.size.x,
                height = node.size.y
            }, (i, j, aroundNode) =>
            {
                if (CheckPass(aroundNode))
                {
                    int key = this.GetUtility<IGameUtility>().GenerateKey(new Vector2Int(i, j), MapSize());
                    surroundingElements.TryAdd(key, aroundNode);
                }
            });

            return surroundingElements;
        }

        /// <summary>
        /// 根据位置返回当前位置的节点信息
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public PathfindingMapNode GetPathfindingMapNode(Vector2Int pos)
        {
            return _mapData[pos.x, pos.y];
        }

        public bool CheckPass(PathfindingMapNode pathfindingMapNode)
        {
            if (pathfindingMapNode.terrainType == TerrainType.CAN_PASS)
            {
                return true;
            }

            return false;
        }

        public Vector2Int MapSize()
        {
            return new Vector2Int(_mapData.Width, _mapData.Height);
        }
    }
}