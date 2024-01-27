namespace Game.BehaviourTree
{
    /// <summary>
    /// 组建队伍的节点
    /// </summary>
    public class BuildTeamNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (aiAgent.BuildTeam())
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.FAILURE;
        }

        public override string GetDescription()
        {
            return "组建一支队伍";
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