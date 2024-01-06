using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class PathfindingSystem: AbstractSystem, IPathfindingSystem
    {
        protected override void OnInit()
        {
            
        }

        public PathfindingSingleMessage Pathfinding(Vector2 startPos, Vector2 endPos,
            PathfindingMap map)
        {
            return Pathfinding(this.GetSystem<IMapSystem>().GetGridMapPos(startPos),
                this.GetSystem<IMapSystem>().GetGridMapPos(endPos), map);
        }

        public PathfindingSingleMessage Pathfinding(Vector2Int startPos, Vector2Int endPos,
            PathfindingMap map)
        {
            if (!map.IsWithinBounds(startPos) || !map.IsWithinBounds(endPos))
            {
                return null;
            }

            if (startPos.Equals(endPos))
            {
                return null;
            }

            Dictionary<int, PathfindingFindNode> openDictionary = new Dictionary<int, PathfindingFindNode>();
            List<int> openDictionaryKey = new List<int>();
            openDictionaryKey.Add(0); //从第二个位置存储
            Dictionary<int, PathfindingFindNode> closeDictionary = new Dictionary<int, PathfindingFindNode>();
            PathfindingMapNode startNode = map.GetPathfindingMapNode(startPos);
            PathfindingMapNode endNode = map.GetPathfindingMapNode(endPos);
            closeDictionary.Add(this.GetUtility<IGameUtility>().GenerateKey(startNode.nodeRect.position, map.MapSize()),
                CreatePathfindingFindNode(startNode, 0, ManhattanDistance(startNode.nodeRect.center, endNode.nodeRect.center),
                    null));

            while (true)
            {
                PathfindingFindNode findNode = closeDictionary.Values.Last();
                Dictionary<int, PathfindingMapNode> pathfindingMapAroundNodes = findNode.pathfindingMapNode.aroundNode;
                List<int> pathfindingMapAroundNodeKey = new List<int>(pathfindingMapAroundNodes.Keys);
                for (int i = 0; i < pathfindingMapAroundNodeKey.Count; i++)
                {
                    PathfindingMapNode mapNode = pathfindingMapAroundNodes[pathfindingMapAroundNodeKey[i]];
                    int key = this.GetUtility<IGameUtility>().GenerateKey(mapNode.nodeRect.position, map.MapSize());
                    if (!closeDictionary.ContainsKey(key)) //所有被加入到close字典中的都不会再被加入到open字典
                    {
                        if (OpenDictionaryAdd(openDictionary, openDictionaryKey, key,
                                CreatePathfindingFindNode(mapNode,
                                    AdjacentEuclideanDistance(mapNode.nodeRect.center,
                                        findNode.pathfindingMapNode.nodeRect.center) +
                                    findNode.lengthToStart, ManhattanDistance(mapNode.nodeRect.center, endNode.nodeRect.center),
                                    findNode)))
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
                CloseDictionaryAdd(closeDictionary,
                    this.GetUtility<IGameUtility>().GenerateKey(minNode.pathfindingMapNode.nodeRect.position, map.MapSize()),
                    minNode);
                OpenListAddRemove(openDictionary, openDictionaryKey);
                if (minNode.pathfindingMapNode.nodeRect.position == endNode.nodeRect.position)
                {
                    break;
                }
            }

            List<PathfindingMapNode> res = new List<PathfindingMapNode>();
            List<float> length = new List<float>();
            PathfindingFindNode node = closeDictionary.Values.Last();
            while (true)
            {
                res.Insert(0, node.pathfindingMapNode);
                if (node.fatherNode != null)
                {
                    length.Insert(0, node.lengthToStart - node.fatherNode.lengthToStart);
                    node = node.fatherNode;
                }
                else
                {
                    length.Insert(0, 0);
                    break;
                }
            }

            PathfindingSingleMessage pathfindingSingleMessage = new PathfindingSingleMessage(res, length);
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
                pathfindingMapNode = pathfindingMapNode,
                lengthToStart = lengthToStart,
                lengthToEnd = lengthToEnd,
                totalLength = lengthToStart + lengthToEnd,
                fatherNode = fatherNode
            };
            return findNode;
        }

        /// <summary>
        /// 获取两个点的曼哈顿距离
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private float ManhattanDistance(Vector2 startPos, Vector2 endPos)
        {
            return Math.Abs(endPos.x - startPos.x) + Math.Abs(endPos.y - startPos.y);
        }

        /// <summary>
        /// 计算两个相邻点的欧里几得距离
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        private float AdjacentEuclideanDistance(Vector2 startPos, Vector2 endPos)
        {
            return (float)Math.Sqrt(Math.Abs(endPos.x - startPos.x) * Math.Abs(endPos.x - startPos.x) +
                                    Math.Abs(endPos.y - startPos.y) * Math.Abs(endPos.y - startPos.y));
        }

        private bool OpenDictionaryAdd(Dictionary<int, PathfindingFindNode> dictionary, List<int> list, int key,
            PathfindingFindNode node)
        {
            if (dictionary.ContainsKey(key))
            {
                PathfindingFindNode oldNode = dictionary[key];
                if (node.totalLength < oldNode.totalLength)
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
                if (node.totalLength < oldNode.totalLength)
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
                if (fatherFindNode.totalLength <= node.totalLength)
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
                    if (childFindNode1.totalLength > childFindNode2.totalLength)
                    {
                        if (node.totalLength > childFindNode2.totalLength)
                        {
                            (list[count], list[childNode2]) = (list[childNode2], list[count]);
                            count = childNode2;
                            continue;
                        }

                        if (node.totalLength > childFindNode1.totalLength)
                        {
                            (list[count], list[childNode1]) = (list[childNode1], list[count]);
                            count = childNode1;
                            continue;
                        }
                    }
                    else
                    {
                        if (node.totalLength > childFindNode1.totalLength)
                        {
                            (list[count], list[childNode1]) = (list[childNode1], list[count]);
                            count = childNode1;
                            continue;
                        }

                        if (node.totalLength > childFindNode2.totalLength)
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
                    if (node.totalLength > childFindNode1.totalLength)
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