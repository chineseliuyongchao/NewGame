namespace Game.BehaviourTree
{
    /// <summary>
    /// 选择节点，顺序执行所有的节点，如果一个节点失败就执行下一个，直到有一个成功，如果全部子节点都失败，就返回失败
    /// </summary>
    public class SelectorNode : BaseCompositeNode
    {
        private int _current;

        protected override void OnStart()
        {
            _current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            var child = children[_current];
            switch (child.Update())
            {
                case BehaviourTreeState.RUNNING:
                    return BehaviourTreeState.RUNNING;
                case BehaviourTreeState.FAILURE:
                    _current++;
                    break;
                case BehaviourTreeState.SUCCESS:
                    return BehaviourTreeState.SUCCESS;
            }

            return _current == children.Count ? BehaviourTreeState.FAILURE : BehaviourTreeState.RUNNING;
        }

        public override string GetDescription()
        {
            return "顺序执行所有子节点，直到有子节点成功";
        }
    }
}