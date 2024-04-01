using System;
using System.Collections.Generic;
using Game.Family;
using Game.GameBase;
using Game.GameUtils;
using Game.Map;
using Game.Town;
using QFramework;
using UnityEngine;

namespace Game.Team
{
    /// <summary>
    /// 所有队伍的基类（可以在大地图移动的单位均视作队伍）
    /// </summary>
    public abstract class BaseTeam : BaseGameController
    {
        /// <summary>
        /// 移动经过位置列表
        /// </summary>
        protected List<Vector2> movePosList;

        /// <summary>
        /// 移动结束以后的事件
        /// </summary>
        private MoveCloseBack _moveEndCallBack;

        private int _teamId;

        public int TeamId => _teamId;

        protected int CurrentIndex { get; set; }

        private TeamData _teamData;

        public TeamData TeamData => _teamData;

        protected override void OnInit()
        {
            base.OnInit();
            movePosList = new List<Vector2>();
        }

        public void InitTeam(int teamId)
        {
            _teamId = teamId;
            _teamData = this.GetModel<ITeamModel>().TeamData[teamId];
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<SoonSaveFileEvent>(_ =>
            {
                this.GetModel<ITeamModel>().TeamData[TeamId].pos = transform.position;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            UpdateTeam();
        }

        /// <summary>
        /// 更新队伍人数
        /// </summary>
        /// <param name="soldierStructure"></param>
        public void UpdateTeamNum(SoldierStructure soldierStructure)
        {
            _teamData.number += soldierStructure.num;
        }

        protected virtual void UpdateTeam()
        {
            if (this.GetModel<IGameModel>().TimeIsPass)
            {
                if (CurrentIndex < movePosList.Count)
                {
                    // 计算当前位置到目标位置的插值
                    transform.position = Vector2.MoveTowards(transform.position, movePosList[CurrentIndex],
                        MoveSpeed() * Time.deltaTime);
                    // 检查是否已经接近目标位置
                    if (Vector2.Distance(transform.position, movePosList[CurrentIndex]) < 0.1f)
                    {
                        // 移动到下一个目标位置
                        CurrentIndex++;
                        // 如果已经到达最后一个位置，执行移动结束后的方法
                        if (CurrentIndex == movePosList.Count)
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
        protected void SetMoveTarget(Vector2 startPos, Vector2 endPos, MoveCloseBack callBack)
        {
            movePosList.Clear();
            CurrentIndex = 0;
            _moveEndCallBack = callBack;
            MoveTarget(startPos, endPos);
        }

        /// <summary>
        /// 使用doTween设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos">角色当前地图位置</param>
        /// <param name="endPos">目标地图位置</param>
        private void MoveTarget(Vector2 startPos, Vector2 endPos)
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
                    movePosList.Add(pos);
                }
            }
            else if (type == PathfindingResultType.DEST_NO_CHANGE) //在同一个网格内移动
            {
                movePosList.Add(this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent, endPos));
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
        protected virtual void ArriveInTown(int townId)
        {
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
        protected virtual float MoveSpeed()
        {
            return GameConstant.BASE_MOVE_SPEED;
        }

        /// <summary>
        /// 设置队伍状态
        /// </summary>
        /// <param name="teamType"></param>
        protected void SetTeamType(TeamType teamType)
        {
            TeamData data = this.GetModel<ITeamModel>().TeamData[TeamId];
            TeamType oldType = data.teamType;
            if (teamType == TeamType.HUT_TOWN)
            {
                if (oldType != TeamType.HUT_TOWN)
                {
                    //进入聚落
                    data.townId = data.targetTownId;
                    this.GetModel<IFamilyModel>().RoleData[data.generalRoleId].townId = data.targetTownId;
                    this.GetModel<ITownModel>().TownData[data.targetTownId].storage.townRoleS.Add(data.generalRoleId);
                }
            }
            else
            {
                if (oldType == TeamType.HUT_TOWN)
                {
                    //离开聚落
                    ITownModel townModel = this.GetModel<ITownModel>();
                    if (townModel.TownData.ContainsKey(data.townId))
                    {
                        if (townModel.TownData[data.townId].storage.townRoleS.Contains(data.generalRoleId))
                        {
                            townModel.TownData[data.townId].storage.townRoleS.Remove(data.generalRoleId);
                        }
                        else
                        {
                            Debug.LogError("聚落：" + townModel.TownData[data.targetTownId].storage.name + "没有加入军队：" +
                                           this.GetModel<IFamilyModel>().RoleData[data.generalRoleId].roleName +
                                           "的军队，但是要从聚落中移除");
                        }
                    }
                    else
                    {
                        Debug.LogError("人物：" + this.GetModel<IFamilyModel>().RoleData[data.generalRoleId].roleName +
                                       "的军队没有设置一个聚落为目标，但是要从聚落离开");
                    }

                    data.townId = 0;
                    this.GetModel<IFamilyModel>().RoleData[data.generalRoleId].townId = 0;
                }
            }

            this.GetModel<ITeamModel>().TeamData[TeamId].teamType = teamType;
        }
    }
}