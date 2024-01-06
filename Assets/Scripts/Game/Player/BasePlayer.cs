using DG.Tweening;
using Game.Town;
using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
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
            PathfindingSingleMessage message =
                PathfindingController.Singleton.Pathfinding(startPos, endPos, this.GetModel<IMapModel>().Map);
            if (message != null)
            {
                for (int i = 0; i < message.pathfindingResult.Count - 1; i++)
                {
                    Vector3 pos = this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent,
                        this.GetSystem<IMapSystem>().GetGridToMapPos(MoveNextPos(message.pathfindingResult[i].nodeRect,
                            message.pathfindingResult[i + 1].nodeRect)));
                    _sequence.Append(transform.DOMove(pos, 0.1f * message.length[i]).SetEase(Ease.Linear));
                }

                _sequence.Append(transform.DOMove(endPos, 1).SetEase(Ease.Linear));

                if (callBack != null)
                {
                    _sequence.AppendCallback(() => { callBack(); });
                }
            }
        }

        /// <summary>
        /// 获取要到达的下一个网格位置（当前节点和下一个要到达的节点的交界处）
        /// </summary>
        /// <param name="nodeRect">当前到达的节点</param>
        /// <param name="nextNodeRect">下一个要到达的节点</param>
        /// <returns></returns>
        private Vector2 MoveNextPos(RectInt nodeRect, RectInt nextNodeRect)
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
                float posY;
                if (nodeRect.height > nextNodeRect.height)
                {
                    posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nextNodeRect.y,
                        nextNodeRect.yMax, (nodeRect.y + nodeRect.yMax) / 2);
                }
                else
                {
                    posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                        nodeRect.yMax, (nextNodeRect.y + nextNodeRect.yMax) / 2);
                }

                return new Vector2(nodeRect.xMax, posY);
            }

            if (nodeRect.x == nextNodeRect.xMax) //下一个节点在当前节点的左边
            {
                float posY;
                if (nodeRect.height > nextNodeRect.height)
                {
                    posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nextNodeRect.y,
                        nextNodeRect.yMax, (nodeRect.y + nodeRect.yMax) / 2);
                }
                else
                {
                    posY = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.y,
                        nodeRect.yMax, (nextNodeRect.y + nextNodeRect.yMax) / 2);
                }

                return new Vector2(nodeRect.x, posY);
            }

            if (nodeRect.yMax == nextNodeRect.y) //下一个节点在当前节点的上边
            {
                float posX;
                if (nodeRect.width > nextNodeRect.width)
                {
                    posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nextNodeRect.x,
                        nextNodeRect.xMax, (nodeRect.x + nodeRect.xMax) / 2);
                }
                else
                {
                    posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                        nodeRect.xMax, (nextNodeRect.x + nextNodeRect.xMax) / 2);
                }

                return new Vector2(posX, nodeRect.yMax);
            }

            if (nodeRect.y == nextNodeRect.yMax) //下一个节点在当前节点的下边
            {
                float posX;
                if (nodeRect.width > nextNodeRect.width)
                {
                    posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nextNodeRect.x,
                        nextNodeRect.xMax, (nodeRect.x + nodeRect.xMax) / 2);
                }
                else
                {
                    posX = this.GetUtility<IGameUtility>().PointClosestToAPointOnALineSegment(nodeRect.x,
                        nodeRect.xMax, (nextNodeRect.x + nextNodeRect.xMax) / 2);
                }

                return new Vector2(posX, nodeRect.y);
            }

            // 如果两个矩形不相邻，返回 Vector2.zero
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