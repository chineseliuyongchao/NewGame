using System;
using System.Collections.Generic;
using Game.Town;
using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game.Player
{
    /// <summary>
    /// 所有游戏角色的基类
    /// </summary>
    public class BasePlayer : BaseGameController
    {
        /// <summary>
        /// 移动经过位置列表
        /// </summary>
        private List<Vector2> _movePosList;

        /// <summary>
        /// 移动结束以后得事件
        /// </summary>
        private MoveCloseBack _moveEndCallBack;

        protected override void OnInit()
        {
            base.OnInit();
            _movePosList = new List<Vector2>();
        }

        private int _currentIndex;

        private void Update()
        {
            if (this.GetModel<IGameModel>().TimeIsPass)
            {
                if (_currentIndex < _movePosList.Count)
                {
                    // 计算当前位置到目标位置的插值
                    transform.position = Vector2.MoveTowards(transform.position, _movePosList[_currentIndex],
                        MoveSpeed() * Time.deltaTime);
                    // 检查是否已经接近目标位置
                    if (Vector2.Distance(transform.position, _movePosList[_currentIndex]) < 0.1f)
                    {
                        // 移动到下一个目标位置
                        _currentIndex++;
                        // 如果已经到达最后一个位置，执行移动结束后的方法
                        if (_currentIndex == _movePosList.Count)
                        {
                            if (_moveEndCallBack != null)
                            {
                                _moveEndCallBack();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="callBack"></param>
        protected void Move(Vector2 startPos, Vector2 endPos, MoveCloseBack callBack)
        {
            _movePosList.Clear();
            _currentIndex = 0;
            _moveEndCallBack = callBack;
            PlayerMove(startPos, endPos);
        }

        /// <summary>
        /// 使用doTween设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos">角色当前地图位置</param>
        /// <param name="endPos">目标地图位置</param>
        private void PlayerMove(Vector2 startPos, Vector2 endPos)
        {
            PathfindingSingleMessage message = this.GetSystem<IPathfindingSystem>()
                .Pathfinding(startPos, endPos, this.GetModel<IMapModel>().Map, out PathfindingResultType type);
            List<Vector2> meshPosList = new List<Vector2>();
            if (message != null)
            {
                meshPosList.Add(this.GetSystem<IMapSystem>().GetGridMapPos(endPos));
                for (int i = message.pathfindingResult.Count - 2; i >= 0; i--) //从终点开始计算每个路径点的网格位置
                {
                    Vector2 pos = MoveNextPos(message.pathfindingResult[i].nodeRect,
                        message.pathfindingResult[i + 1].nodeRect, meshPosList[0]);
                    meshPosList.Insert(0, pos);
                }

                meshPosList.Insert(0, this.GetSystem<IMapSystem>().GetGridMapPos(startPos)); //将起点加入方便优化路径

                for (int i = message.pathfindingResult.Count - 2; i >= 0; i--) //通过上一个点和下一个点优化除了起点和终点以外所有点的位置
                {
                    meshPosList[i + 1] = OptimizeMovePos(message.pathfindingResult[i].nodeRect,
                        message.pathfindingResult[i + 1].nodeRect, meshPosList[i], meshPosList[i + 1],
                        meshPosList[i + 2]);
                }

                for (int i = 1; i < meshPosList.Count; i++) //将每个路径点的网格位置转成实际位置并且添加到移动列表中
                {
                    Vector2 pos = this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent,
                        this.GetSystem<IMapSystem>().GetGridToMapPos(meshPosList[i]));
                    _movePosList.Add(pos);
                }
            }
            else if (type == PathfindingResultType.DEST_NO_CHANGE) //在同一个网格内移动
            {
                _movePosList.Add(this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent, endPos));
            }
        }

        /// <summary>
        /// 获取当前要达到的网格位置（当前节点和下一个要到达的节点的交界处）
        /// </summary>
        /// <param name="nodeRect">当前到达的节点</param>
        /// <param name="nextNodeRect">下一个要到达的节点</param>
        /// <param name="nextPos">下一个达到的网格位置，当前位置要尽可能离下一个网格位置更近</param>
        /// <returns></returns>
        private Vector2 MoveNextPos(RectInt nodeRect, RectInt nextNodeRect, Vector2 nextPos)
        {
            if (nodeRect.xMax == nextNodeRect.x && nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的右下角
            {
                return new Vector2(nodeRect.xMax, nodeRect.y);
            }

            if (nodeRect.x == nextNodeRect.xMax && nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的左下角
            {
                return new Vector2(nodeRect.x, nodeRect.y);
            }

            if (nodeRect.xMax == nextNodeRect.x && nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的右上角
            {
                return new Vector2(nodeRect.xMax, nodeRect.yMax);
            }

            if (nodeRect.x == nextNodeRect.xMax && nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的左上角
            {
                return new Vector2(nodeRect.x, nodeRect.yMax);
            }

            if (nodeRect.xMax == nextNodeRect.x) //下一个节点在当前节点的右边
            {
                float posY = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                    nodeRect.yMax, (int)nextPos.y); //nextPos.y肯定在nextNodeRect范围内，所以只要确保在nodeRect范围内即可
                return new Vector2(nodeRect.xMax, posY);
            }

            if (nodeRect.x == nextNodeRect.xMax) //下一个节点在当前节点的左边
            {
                float posY = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                    nodeRect.yMax, (int)nextPos.y);
                return new Vector2(nodeRect.x, posY);
            }

            if (nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的上边
            {
                float posX = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                    nodeRect.xMax, (int)nextPos.x);
                return new Vector2(posX, nodeRect.yMax);
            }

            if (nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的下边
            {
                float posX = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                    nodeRect.xMax, (int)nextPos.x);
                return new Vector2(posX, nodeRect.y);
            }

            // 如果两个矩形不相邻，返回 Vector2.zero，理论上不可能
            return Vector2.zero;
        }

        /// <summary>
        /// 通过上一个点和下一个点的位置优化移动路径点的位置
        /// </summary>
        /// <param name="nodeRect">当前到达的节点</param>
        /// <param name="nextNodeRect">下一个要到达的节点</param>
        /// <param name="lastPos">上一个到达的网格位置</param>
        /// <param name="nowPos">当前网格位置</param>
        /// <param name="nextPos">下一个达到的网格位置</param>
        /// <returns></returns>
        private Vector2 OptimizeMovePos(RectInt nodeRect, RectInt nextNodeRect, Vector2 lastPos, Vector2 nowPos,
            Vector2 nextPos)
        {
            if (nodeRect.xMax == nextNodeRect.x && nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的右下角
            {
                return nowPos;
            }

            if (nodeRect.x == nextNodeRect.xMax && nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的左下角
            {
                return nowPos;
            }

            if (nodeRect.xMax == nextNodeRect.x && nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的右上角
            {
                return nowPos;
            }

            if (nodeRect.x == nextNodeRect.xMax && nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的左上角
            {
                return nowPos;
            }

            if (nodeRect.xMax == nextNodeRect.x) //下一个节点在当前节点的右边
            {
                int posY = this.GetUtility<IMathUtility>().Intersection(lastPos, nextPos, nodeRect.xMax, -1);
                posY = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(
                    Math.Max(nodeRect.y, nextNodeRect.y), Math.Min(nodeRect.yMax, nextNodeRect.yMax),
                    posY == -1 ? (int)nextPos.y : posY); //计算出的交点位置不确定，需要确保同时在nodeRect和nextNodeRect范围内
                return new Vector2(nodeRect.xMax, posY);
            }

            if (nodeRect.x == nextNodeRect.xMax) //下一个节点在当前节点的左边
            {
                int posY = this.GetUtility<IMathUtility>().Intersection(lastPos, nextPos, nodeRect.x, -1);
                posY = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(
                    Math.Max(nodeRect.y, nextNodeRect.y), Math.Min(nodeRect.yMax, nextNodeRect.yMax),
                    posY == -1 ? (int)nextPos.y : posY);
                return new Vector2(nodeRect.x, posY);
            }

            if (nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的上边
            {
                int posX = this.GetUtility<IMathUtility>().Intersection(lastPos, nextPos, -1, nodeRect.yMax);
                posX = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(
                    Math.Max(nodeRect.x, nextNodeRect.x), Math.Min(nodeRect.xMax, nextNodeRect.xMax),
                    posX == -1 ? (int)nextPos.x : posX);
                return new Vector2(posX, nodeRect.yMax);
            }

            if (nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的下边
            {
                int posX = this.GetUtility<IMathUtility>().Intersection(lastPos, nextPos, -1, nodeRect.y);
                posX = this.GetUtility<IMathUtility>().PointClosestToAPointOnALineSegment(
                    Math.Max(nodeRect.x, nextNodeRect.x), Math.Min(nodeRect.xMax, nextNodeRect.xMax),
                    posX == -1 ? (int)nextPos.x : posX);
                return new Vector2(posX, nodeRect.y);
            }

            // 如果两个矩形不相邻，返回 Vector2.zero，理论上不可能
            return Vector2.zero;
        }


        /// <summary>
        /// 游戏角色移动到某个聚落
        /// </summary>
        /// <param name="baseTown"></param>
        protected virtual void MoveToTown(BaseTown baseTown)
        {
            Debug.Log("moveToTown:  " + baseTown.name);
        }

        /// <summary>
        /// 获取起点地图位置
        /// </summary>
        /// <returns></returns>
        protected Vector2 GetStartMapPos()
        {
            return this.GetSystem<IMapSystem>().GetMapPos(transform);
        }

        /// <summary>
        /// 获取移动速度
        /// </summary>
        /// <returns></returns>
        protected float MoveSpeed()
        {
            return 5;
        }
    }
}