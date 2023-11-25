using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统（使用没有二叉堆的方法进行查找）
    /// </summary>
    public class PathfindingControllerOld : ViewController, ISingleton
    {
        public static PathfindingControllerOld Singleton => MonoSingletonProperty<PathfindingControllerOld>.Instance;

        public void OnSingletonInit()
        {
        }

        public PathfindingSingleMessage Pathfinding(IntVector2 startPos, IntVector2 endPos,
            PathfindingMap map)
        {
            if (!map.IsWithinBounds(startPos) || !map.IsWithinBounds(endPos))
            {
                return null;
            }

            Dictionary<int, PathfindingFindNode> openDictionary = new Dictionary<int, PathfindingFindNode>();
            Dictionary<int, PathfindingFindNode> closeDictionary = new Dictionary<int, PathfindingFindNode>();
            closeDictionary.Add(GenerateKey(startPos, map.MapSize()), CreatePathfindingFindNode(
                map.GetPathfindingMapNode(startPos), 0,
                ManhattanDistance(startPos, endPos), null));

            while (true)
            {
                PathfindingFindNode findNode = closeDictionary.Values.Last();
                List<PathfindingMapNode> pathfindingMapNodes =
                    map.FindAroundNode(findNode.PathfindingMapNode.Pos);
                for (int i = 0; i < pathfindingMapNodes.Count; i++)
                {
                    PathfindingMapNode mapNode = pathfindingMapNodes[i];
                    int key = GenerateKey(mapNode.Pos, map.MapSize());
                    if (!closeDictionary.ContainsKey(key)) //所有被加入到close字典中的都不会再被加入到open字典
                    {
                        DictionaryAdd(openDictionary, key,
                            CreatePathfindingFindNode(mapNode,
                                AdjacentEuclideanDistance(mapNode.Pos, findNode.PathfindingMapNode.Pos) +
                                findNode.LengthToStart, ManhattanDistance(mapNode.Pos, endPos), findNode));
                    }
                }

                if (openDictionary.Count == 0)
                {
                    break;
                }

                PathfindingFindNode minNode =
                    openDictionary.OrderBy(kvp => kvp.Value.TotalLength).FirstOrDefault().Value;
                DictionaryAdd(closeDictionary, GenerateKey(minNode.PathfindingMapNode.Pos, map.MapSize()), minNode);
                int keyToRemove = openDictionary.First(kvp => kvp.Value == minNode).Key;
                openDictionary.Remove(keyToRemove);
                if (minNode.PathfindingMapNode.Pos == endPos)
                {
                    break;
                }
            }

            List<PathfindingMapNode> res = new List<PathfindingMapNode>();
            PathfindingFindNode node = closeDictionary.Values.Last();
            while (true)
            {
                res.Add(node.PathfindingMapNode);
                if (node.FatherNode != null)
                {
                    node = node.FatherNode;
                }
                else
                {
                    break;
                }
            }

            PathfindingSingleMessage pathfindingSingleMessage = new PathfindingSingleMessage(res);
            return pathfindingSingleMessage;
        }

        /// <summary>
        /// 生成一个路径节点
        /// </summary>
        /// <param name="pathfindingMapNode"></param>
        /// <param name="lengthToStart"></param>
        /// <param name="lengthToEnd"></param>
        /// <param name="fatherNode"></param>
        /// <returns></returns>
        private PathfindingFindNode CreatePathfindingFindNode(PathfindingMapNode pathfindingMapNode,
            float lengthToStart, float lengthToEnd, PathfindingFindNode fatherNode)
        {
            PathfindingFindNode findNode = new PathfindingFindNode
            {
                PathfindingMapNode = pathfindingMapNode,
                LengthToStart = lengthToStart,
                LengthToEnd = lengthToEnd,
                TotalLength = lengthToStart + lengthToEnd,
                FatherNode = fatherNode
            };
            return findNode;
        }

        /// <summary>
        /// 获取两个点的曼哈顿距离
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private int ManhattanDistance(IntVector2 startPos, IntVector2 endPos)
        {
            return Math.Abs(endPos.X - startPos.X) + Math.Abs(endPos.Y - startPos.Y);
        }

        /// <summary>
        /// 计算两个相邻点的欧里几得距离
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private float AdjacentEuclideanDistance(IntVector2 startPos, IntVector2 endPos)
        {
            int num = ManhattanDistance(startPos, endPos);
            if (num == 2)
            {
                return 1.4f;
            }

            return 1;
        }

        /// <summary>
        /// 根据位置和地图尺寸生成每个位置单独的key
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int GenerateKey(IntVector2 pos, IntVector2 length)
        {
            return pos.X * length.Y + pos.Y;
        }

        private void DictionaryAdd(Dictionary<int, PathfindingFindNode> dictionary, int key, PathfindingFindNode node)
        {
            if (dictionary.ContainsKey(key))
            {
                PathfindingFindNode oldNode = dictionary[key];
                if (node.TotalLength < oldNode.TotalLength)
                {
                    dictionary[key] = node;
                }
            }
            else
            {
                dictionary.Add(key, node);
            }
        }
    }
}