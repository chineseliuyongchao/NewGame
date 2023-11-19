using System.Collections.Generic;
using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统的地图，记录所有节点
    /// </summary>
    public class PathfindingMap
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

        public bool IsWithinBounds(IntVector2 pos)
        {
            return _mapData.IsWithinBounds(pos);
        }

        /// <summary>
        /// 获取一个节点的周围八个节点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public List<PathfindingMapNode> FindAroundNode(IntVector2 pos)
        {
            List<PathfindingMapNode> surroundingElements = new List<PathfindingMapNode>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    int newX = pos.X + i;
                    int newY = pos.Y + j;
                    PathfindingMapNode element = _mapData[newX, newY];
                    if (element != default && CheckPass(element))
                    {
                        surroundingElements.Add(element);
                    }
                }
            }

            return surroundingElements;
        }

        public PathfindingMapNode GetPathfindingMapNode(IntVector2 pos)
        {
            return _mapData[pos.X, pos.Y];
        }

        private bool CheckPass(PathfindingMapNode pathfindingMapNode)
        {
            if (pathfindingMapNode.TerrainType == TerrainType.CAN_PASS)
            {
                return true;
            }

            return false;
        }

        public IntVector2 MapSize()
        {
            return new IntVector2(_mapData.Width, _mapData.Height);
        }
    }
}