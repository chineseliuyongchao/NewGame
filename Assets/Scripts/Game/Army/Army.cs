using Game.BehaviourTree;
using GameQFramework;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Army
{
    /// <summary>
    /// 普通ai军队
    /// </summary>
    public class Army : BaseArmy, IGetBehaviourTree
    {
        public BehaviourTree.BehaviourTree behaviourTree;
        private int _generalId;
        public int ArmyId { get; set; }
        private ArmyBlackBoard _armyBlackBoard;
        private ArmyAiAgent _aiAgent;
        private int _targetTownId;
        private readonly float _patrolRadius = 1; // 巡逻半径

        protected override void OnInit()
        {
            base.OnInit();
            behaviourTree = behaviourTree.Clone();
            _armyBlackBoard = new ArmyBlackBoard
            {
                moveToTown = MoveToTown,
                patrol = Patrol
            };
            behaviourTree.blackboard = _armyBlackBoard;
            _aiAgent = this.AddComponent<ArmyAiAgent>();
            behaviourTree.Bind(_aiAgent);
        }

        protected override void OnControllerStart()
        {
            _aiAgent.Init(_armyBlackBoard);
            behaviourTree.StartTree();
        }

        protected override void UpdateArmy()
        {
            base.UpdateArmy();
            behaviourTree.UpdateTree();
            switch (ArmyType)
            {
                case ArmyType.PATROL:
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
            ArmyType = ArmyType.PATROL;
            _targetTownId = townId;
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}