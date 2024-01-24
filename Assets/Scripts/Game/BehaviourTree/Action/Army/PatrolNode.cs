namespace Game.BehaviourTree
{
    /// <summary>
    /// 巡逻的节点
    /// </summary>
    public class PatrolNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            aiAgent.Patrol();
            return BehaviourTreeState.SUCCESS;
        }

        public override string GetDescription()
        {
            return "巡逻";
        }

        /// <summary>
        /// 返回创建节点时的三级目录
        /// </summary>
        /// <returns></returns>
        public static string FunctionPath()
        {
            return "Army/";
        }
    }
}