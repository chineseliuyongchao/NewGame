namespace Game.BehaviourTree
{
    /// <summary>
    /// 序列节点，顺序执行所有的子节点，如果某一个子节点失败，则失败，如果所有子节点成功，则完成
    /// </summary>
    public class SequenceNode : BaseCompositeNode
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
                    return BehaviourTreeState.FAILURE;
                case BehaviourTreeState.SUCCESS:
                    _current++;
                    break;
            }

            return _current == children.Count  ? BehaviourTreeState.SUCCESS : BehaviourTreeState.RUNNING;
        }
    }
}