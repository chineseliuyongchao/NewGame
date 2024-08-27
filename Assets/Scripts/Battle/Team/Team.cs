using Battle.Map;
using Battle.Town;
using Game.BehaviourTree;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.Team
{
    /// <summary>
    /// 普通ai队伍
    /// </summary>
    public class Team : BaseTeam, IGetBehaviourTree
    {
        public Game.BehaviourTree.BehaviourTree behaviourTree;
        private TeamBlackBoard _teamBlackBoard;
        private TeamAiAgent _aiAgent;
        private readonly float _patrolRadius = 1; // 巡逻半径

        protected override void OnInit()
        {
            base.OnInit();
            behaviourTree = behaviourTree.Clone();
            _teamBlackBoard = new TeamBlackBoard
            {
                moveToTown = MoveToTown,
                patrol = Patrol
            };
            behaviourTree.blackboard = _teamBlackBoard;
            _aiAgent = this.AddComponent<TeamAiAgent>();
            behaviourTree.Bind(_aiAgent);
        }

        protected override void OnControllerStart()
        {
            _aiAgent.Init(_teamBlackBoard);
        }

        protected override void UpdateTeam()
        {
            base.UpdateTeam();
            behaviourTree.UpdateTree();
            switch (this.GetModel<ITeamModel>().TeamData[TeamId].teamType)
            {
                case TeamType.HUT_TOWN:
                    behaviourTree.StartTree();
                    break;
                case TeamType.HUT_FIELD:
                    behaviourTree.StartTree();
                    break;
                case TeamType.MOVE_TO_TOWN:
                    if (movePosList.Count == 0)
                    {
                        MoveToTown(this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId);
                    }

                    break;
                case TeamType.MOVE_TO_TEAM:
                    break;
                case TeamType.PATROL:
                    if (CurrentIndex == movePosList.Count)
                    {
                        float randomAngle = Random.Range(0f, 360f);
                        // 根据新的角度计算新的位置
                        float x = Mathf.Cos(randomAngle) * _patrolRadius;
                        float y = Mathf.Sin(randomAngle) * _patrolRadius;
                        TownCommonData townCommonData = this.GetModel<ITownModel>()
                            .TownCommonData[this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId];
                        SetMoveTarget(GetStartMapPos(), this.GetSystem<IMapSystem>().GetRealPosToMapPos(
                                new Vector2(townCommonData.TownPos[0] + x, townCommonData.TownPos[1] + y)),
                            () => { });
                    }

                    break;
            }
        }

        /// <summary>
        /// 移动到聚落
        /// </summary>
        /// <param name="townId"></param>
        private void MoveToTown(int townId)
        {
            SetTeamType(TeamType.MOVE_TO_TOWN);
            this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId = townId;
            TownCommonData townData = this.GetModel<ITownModel>().TownCommonData[townId];
            SetMoveTarget(GetStartMapPos(),
                this.GetSystem<IMapSystem>().GetRealPosToMapPos(new Vector2(townData.TownPos[0], townData.TownPos[1])),
                () => { ArriveInTown(townId); });
        }

        /// <summary>
        /// 巡逻
        /// </summary>
        /// <param name="townId">巡逻的聚落</param>
        private void Patrol(int townId)
        {
            this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId = townId;
            SetTeamType(TeamType.PATROL);
        }

        protected override void ArriveInTown(int townId)
        {
            SetTeamType(TeamType.HUT_TOWN);
            //临时处理，ai军队到达聚落以后就会征集一次军队
            Conscription(townId);
        }

        /// <summary>
        /// 征兵
        /// </summary>
        /// <param name="townId"></param>
        private void Conscription(int townId)
        {
            ConscriptionData data = this.GetSystem<ITownSystem>().Conscription(townId);
            int num = Random.Range(0, data.canConscription.num);
            SoldierStructure soldierStructure = new SoldierStructure
            {
                num = num
            };
            this.GetSystem<ITeamSystem>().ComputeMoneyWithConscription(soldierStructure, FamilyId);
            data.realConscription(soldierStructure);
            UpdateTeamNum(soldierStructure);
        }

        public Game.BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}