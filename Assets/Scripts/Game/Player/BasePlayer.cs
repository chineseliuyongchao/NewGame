using System.Collections.Generic;
using DG.Tweening;
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
        private Sequence _sequence;

        /// <summary>
        /// 设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="callBack"></param>
        protected void Move(Vector2 startPos, Vector2 endPos, MoveCloseBack callBack)
        {
            if (_sequence == null || !_sequence.active)
            {
                _sequence = DOTween.Sequence();

                PlayerMove(startPos, endPos, callBack);
            }
            else if (!_sequence.IsPlaying())
            {
                _sequence = DOTween.Sequence();
                PlayerMove(startPos, endPos, callBack);
            }
            else
            {
                _sequence.OnKill(() =>
                {
                    _sequence = DOTween.Sequence();
                    PlayerMove(startPos, endPos, callBack);
                });
                _sequence.Kill();
            }
        }

        /// <summary>
        /// 使用doTween设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos">角色当前地图位置</param>
        /// <param name="endPos">目标地图位置</param>
        /// <param name="callBack"></param>
        private void PlayerMove(Vector2 startPos, Vector2 endPos, MoveCloseBack callBack)
        {
            PathfindingSingleMessage message = this.GetSystem<IPathfindingSystem>()
                .Pathfinding(startPos, endPos, this.GetModel<IMapModel>().Map);
            if (message != null)
            {
                List<Vector2> meshPosList = new List<Vector2>(); //记录每一个要到达的位置
                meshPosList.Add(this.GetSystem<IMapSystem>().GetGridMapPos(endPos));
                for (int i = message.pathfindingResult.Count - 2; i >= 0; i--)
                {
                    Vector2 pos = MoveNextPos(message.pathfindingResult[i].nodeRect,
                        message.pathfindingResult[i + 1].nodeRect, meshPosList[0]);
                    meshPosList.Insert(0, pos);
                }

                for (int i = 0; i < meshPosList.Count; i++)
                {
                    Vector2 pos = this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent,
                        this.GetSystem<IMapSystem>().GetGridToMapPos(meshPosList[i]));
                    _sequence.Append(transform.DOMove(pos, 1).SetEase(Ease.Linear));
                }
            }
            else //在同一个网格内移动
            {
                _sequence.Append(transform
                    .DOMove(this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent, endPos), 1)
                    .SetEase(Ease.Linear));
            }

            if (callBack != null)
            {
                _sequence.AppendCallback(() => { callBack(); });
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
                float posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                    nodeRect.yMax, (int)nextPos.y);
                return new Vector2(nodeRect.xMax, posY);
            }

            if (nodeRect.x == nextNodeRect.xMax) //下一个节点在当前节点的左边
            {
                float posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                    nodeRect.yMax, (int)nextPos.y);
                return new Vector2(nodeRect.x, posY);
            }

            if (nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的上边
            {
                float posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                    nodeRect.xMax, (int)nextPos.x);
                return new Vector2(posX, nodeRect.yMax);
            }

            if (nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的下边
            {
                float posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                    nodeRect.xMax, (int)nextPos.x);
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
    }
}