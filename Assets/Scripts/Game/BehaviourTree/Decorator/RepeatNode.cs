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
            child.Update();
            return BehaviourTreeState.RUNNING;
        }
    }
}