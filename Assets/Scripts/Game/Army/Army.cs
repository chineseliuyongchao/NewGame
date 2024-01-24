using Game.BehaviourTree;
using Unity.VisualScripting;

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
            _armyBlackBoard = new ArmyBlackBoard();
            behaviourTree.blackboard = _armyBlackBoard;
            _aiAgent = this.AddComponent<ArmyAiAgent>();
            behaviourTree.Bind(_aiAgent);
        }

        protected override void OnControllerStart()
        {
            _aiAgent.Init(_armyBlackBoard);
            behaviourTree.StartTree();
        }

        private void Update()
        {
            behaviourTree.UpdateTree();
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}