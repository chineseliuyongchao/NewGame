using System;
using System.Collections.Generic;
using System.Linq;
using GameQFramework;
using QFramework;
using Utils;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 寻路系统
    /// </summary>
    public class PathfindingController : BaseGameController, ISingleton
    {
        public static PathfindingController Singleton => MonoSingletonProperty<PathfindingController>.Instance;

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
            List<int> openDictionaryKey = new List<int>();
            openDictionaryKey.Add(0); //从第二个位置存储
            Dictionary<int, PathfindingFindNode> closeDictionary = new Dictionary<int, PathfindingFindNode>();
            closeDictionary.Add(GenerateKey(startPos, map.MapSize()), CreatePathfindingFindNode(
                map.GetPathfindingMapNode(startPos), 0,
                ManhattanDistance(startPos, endPos), null));

            while (true)
            {
                PathfindingFindNode findNode = closeDictionary.Values.Last();
                List<PathfindingMapNode> pathfindingMapNodes = findNode.PathfindingMapNode.AroundNode;
                for (int i = 0; i < pathfindingMapNodes.Count; i++)
                {
                    PathfindingMapNode mapNode = pathfindingMapNodes[i];
                    int key = GenerateKey(mapNode.Pos, map.MapSize());
                    if (!closeDictionary.ContainsKey(key)) //所有被加入到close字典中的都不会再被加入到open字典
                    {
                        if (OpenDictionaryAdd(openDictionary, openDictionaryKey, key,
                                CreatePathfindingFindNode(mapNode,
                                    AdjacentEuclideanDistance(mapNode.Pos, findNode.PathfindingMapNode.Pos) +
                                    findNode.LengthToStart, ManhattanDistance(mapNode.Pos, endPos), findNode)))
                        {
                            //只有加入了之前没有的节点才需要在list中添加新的key
                            OpenListAdd(openDictionary, openDictionaryKey, key);
                        }
                    }
                }

                if (openDictionary.Count == 0)
                {
                    break;
                }

                PathfindingFindNode minNode = openDictionary[openDictionaryKey[1]];
                CloseDictionaryAdd(closeDictionary, GenerateKey(minNode.PathfindingMapNode.Pos, map.MapSize()),
                    minNode);
                OpenListAddRemove(openDictionary, openDictionaryKey);
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

        private bool OpenDictionaryAdd(Dictionary<int, PathfindingFindNode> dictionary, List<int> list, int key,
            PathfindingFindNode node)
        {
            if (dictionary.ContainsKey(key))
            {
                PathfindingFindNode oldNode = dictionary[key];
                if (node.TotalLength < oldNode.TotalLength)
                {
                    dictionary[key] = node;
                    list.Remove(key);
                    return true;
                }
            }
            else
            {
                dictionary.Add(key, node);
                return true;
            }

            return false;
        }

        private void CloseDictionaryAdd(Dictionary<int, PathfindingFindNode> dictionary, int key,
            PathfindingFindNode node)
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

        /// <summary>
        /// 二叉堆插入key值
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="list"></param>
        /// <param name="key"></param>
        private void OpenListAdd(Dictionary<int, PathfindingFindNode> dictionary, List<int> list, int key)
        {
            int count = list.Count;
            list.Add(key);
            while (true)
            {
                if (count <= 1)
                {
                    break;
                }

                int fatherNode = count >> 1;
                PathfindingFindNode fatherFindNode = dictionary[list[fatherNode]];
                PathfindingFindNode node = dictionary[list[count]];
                if (fatherFindNode.TotalLength <= node.TotalLength)
                {
                    break;
                }

                (list[count], list[fatherNode]) = (list[fatherNode], list[count]);
                count = fatherNode;
            }
        }

        /// <summary>
        /// 二叉堆移除key值（直接移除第二个，第一个占位）
        /// </summary>
        private void OpenListAddRemove(Dictionary<int, PathfindingFindNode> dictionary, List<int> list)
        {
            int count = list.Count - 1;
            dictionary.Remove(list[1]); //最小的值被去除，在字典中移除对应值
            list[1] = list[count];
            list.RemoveAt(count); //list中最后一个key被移动到第一个
            count = 1;
            if (list.Count <= count)
            {
                return;
            }

            while (true)
            {
                PathfindingFindNode node = dictionary[list[count]];
                int childNode1 = count << 1;
                int childNode2 = childNode1 + 1;
                if (childNode2 < list.Count)
                {
                    PathfindingFindNode childFindNode1 = dictionary[list[childNode1]];
                    PathfindingFindNode childFindNode2 = dictionary[list[childNode2]];
                    if (childFindNode1.TotalLength > childFindNode2.TotalLength)
                    {
                        if (node.TotalLength > childFindNode2.TotalLength)
                        {
                            (list[count], list[childNode2]) = (list[childNode2], list[count]);
                            count = childNode2;
                            continue;
                        }

                        if (node.TotalLength > childFindNode1.TotalLength)
                        {
                            (list[count], list[childNode1]) = (list[childNode1], list[count]);
                            count = childNode1;
                            continue;
                        }
                    }
                    else
                    {
                        if (node.TotalLength > childFindNode1.TotalLength)
                        {
                            (list[count], list[childNode1]) = (list[childNode1], list[count]);
                            count = childNode1;
                            continue;
                        }

                        if (node.TotalLength > childFindNode2.TotalLength)
                        {
                            (list[count], list[childNode2]) = (list[childNode2], list[count]);
                            count = childNode2;
                            continue;
                        }
                    }
                }
                else if (childNode1 < list.Count)
                {
                    PathfindingFindNode childFindNode1 = dictionary[list[childNode1]];
                    if (node.TotalLength > childFindNode1.TotalLength)
                    {
                        (list[count], list[childNode1]) = (list[childNode1], list[count]);
                        count = childNode1;
                        continue;
                    }
                }

                break;
            }
        }
    }
}