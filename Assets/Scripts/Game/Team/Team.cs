using Game.BehaviourTree;
using GameQFramework;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Team
{
    /// <summary>
    /// 普通ai队伍
    /// </summary>
    public class Team : BaseTeam, IGetBehaviourTree
    {
        public BehaviourTree.BehaviourTree behaviourTree;
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
                    if (CurrentIndex == movePosList.Count)
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
            this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.MOVE_TO_TOWN;
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
            this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.PATROL;
            this.GetModel<ITeamModel>().TeamData[TeamId].targetTownId = townId;
        }

        protected override void ArriveInTown(int townId)
        {
            this.GetModel<ITeamModel>().TeamData[TeamId].teamType = TeamType.HUT_TOWN;
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}