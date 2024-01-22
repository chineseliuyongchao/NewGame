namespace Game.BehaviourTree
{
    /// <summary>
    /// 组建军队的节点
    /// </summary>
    public class BuildArmyNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (aiAgent.BuildArmy())
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.FAILURE;
        }

        public override string GetDescription()
        {
            return "组建一支军队";
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