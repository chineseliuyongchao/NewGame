namespace Game.BehaviourTree
{
    /// <summary>
    /// 并行节点，同时执行所有的节点，直到全部成功或者某一个失败
    /// </summary>
    public class ParallelNode : BaseCompositeNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            BehaviourTreeState state=BehaviourTreeState.SUCCESS;
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                switch (child.Update())
                {
                    case BehaviourTreeState.FAILURE:
                        return BehaviourTreeState.FAILURE;
                    case BehaviourTreeState.RUNNING:
                        state = BehaviourTreeState.RUNNING;
                        break;
                }
            }

            return state;
        }

        public override string GetDescription()
        {
            return "同时执行所有的节点，直到全部成功或者某一个失败";
        }
    }
}