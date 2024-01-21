namespace Game.BehaviourTree
{
    /// <summary>
    /// 重复节点，让子节点不返回任何内容，只是重复执行
    /// </summary>
    public class RepeatNode : BaseDecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (child.treeState == BehaviourTreeState.FAILURE || child.treeState == BehaviourTreeState.SUCCESS)
            {
                //如果子节点已经执行完，那就重置子节点继续执行
                child.Resetting();
            }

            child.Update();
            return BehaviourTreeState.RUNNING;
        }

        public override string GetDescription()
        {
            return "重复执行子节点";
        }
    }
}