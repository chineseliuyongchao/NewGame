﻿using System.Collections.Generic;
using Game.GameBase;
using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Battle.Map
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
        /// 获取一个节点的周围所有可通行节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public void FindAroundNode(PathfindingMapNode node)
        {
            Dictionary<int, PathfindingMapNode> aroundNodeS = new Dictionary<int, PathfindingMapNode>();
            _mapData.ForEachAround(node.nodeRect, (_, _, aroundNode) =>
            {
                if (CheckPass(aroundNode))
                {
                    int key = this.GetUtility<IGameUtility>()
                        .GenerateKey(new Vector2Int(aroundNode.nodeRect.x, aroundNode.nodeRect.y), MapSize());
                    aroundNodeS.TryAdd(key, aroundNode);
                }
            });
            node.aroundNode = aroundNodeS;
        }

        /// <summary>
        /// 根据网格位置返回当前位置的节点信息
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public PathfindingMapNode GetPathfindingMapNode(Vector2Int pos)
        {
            return _mapData[pos.x, pos.y];
        }

        /// <summary>
        /// 根据网格位置返回最近的可通行的节点信息
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public PathfindingMapNode GetPathfindingLastMapNode(Vector2Int pos)
        {
            RectInt rect = new RectInt(pos.x, pos.y, 1, 1);
            PathfindingMapNode mapNode = null;
            while (true)
            {
                rect.x -= 1;
                rect.y -= 1;
                rect.width += 2;
                rect.height += 2;
                _mapData.ForEach(rect, (_, _, node) =>
                {
                    if (CheckPass(node))
                    {
                        mapNode = node;
                    }
                });
                if (mapNode != null)
                {
                    return mapNode;
                }
            }
        }

        public bool CheckPass(PathfindingMapNode pathfindingMapNode)
        {
            if (pathfindingMapNode == null)
            {
                return false;
            }

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