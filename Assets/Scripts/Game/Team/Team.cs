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
        private int _generalId;
        public int TeamId { get; set; }
        private TeamBlackBoard _teamBlackBoard;
        private TeamAiAgent _aiAgent;
        private int _targetTownId;
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
            behaviourTree.StartTree();
        }

        protected override void UpdateTeam()
        {
            base.UpdateTeam();
            behaviourTree.UpdateTree();
            switch (TeamType)
            {
                case TeamType.PATROL:
                    if (CurrentIndex == movePosList.Count)
                    {
                        float randomAngle = Random.Range(0f, 360f);
                        // 根据新的角度计算新的位置
                        float x = Mathf.Cos(randomAngle) * _patrolRadius;
                        float y = Mathf.Sin(randomAngle) * _patrolRadius;
                        TownCommonData townCommonData = this.GetModel<ITownModel>().TownCommonData[_targetTownId];
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
            TeamType = TeamType.PATROL;
            _targetTownId = townId;
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}