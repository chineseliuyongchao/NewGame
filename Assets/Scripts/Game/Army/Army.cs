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
        private ArmyBlackBoard _armyBlackBoard;
        private ArmyAiAgent _aiAgent;

        protected override void OnInit()
        {
            base.OnInit();
            behaviourTree = behaviourTree.Clone();
            _armyBlackBoard = new ArmyBlackBoard
            {
                moveToTown = MoveToTown
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
        }

        /// <summary>
        /// 移动到聚落
        /// </summary>
        /// <param name="townId"></param>
        private void MoveToTown(int townId)
        {
            TownCommonData townData = this.GetModel<ITownModel>().TownCommonData[townId];
            Move(GetStartMapPos(),
                this.GetSystem<IMapSystem>().GetRealPosToMapPos(new Vector2(townData.TownPos[0], townData.TownPos[1])),
                () => { ArriveInTown(townId); });
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}