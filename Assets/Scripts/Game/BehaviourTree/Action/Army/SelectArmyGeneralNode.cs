namespace Game.BehaviourTree
{
    /// <summary>
    /// 为军队挑选将军的节点
    /// </summary>
    public class SelectArmyGeneralNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (aiAgent.SelectArmyGeneral())
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.FAILURE;
        }

        public override string GetDescription()
        {
            return "为将要组建的军队挑选一位将军";
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