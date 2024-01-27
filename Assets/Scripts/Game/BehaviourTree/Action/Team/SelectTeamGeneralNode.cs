namespace Game.BehaviourTree
{
    /// <summary>
    /// 为队伍挑选将军的节点
    /// </summary>
    public class SelectTeamGeneralNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (aiAgent.SelectTeamGeneral())
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.FAILURE;
        }

        public override string GetDescription()
        {
            return "为将要组建的队伍挑选一位将军";
        }

        /// <summary>
        /// 返回创建节点时的三级目录
        /// </summary>
        /// <returns></returns>
        public static string FunctionPath()
        {
            return "Team/";
        }
    }
}