namespace Game.BehaviourTree
{
    /// <summary>
    /// 判断是否可以组建新队伍的节点
    /// </summary>
    public class CanBuildTeamNode : BaseConditionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        public override string GetDescription()
        {
            return "用于判断是否可以组建新的队伍";
        }

        protected override bool JudgeResult()
        {
            return aiAgent.CanBuildTeam();
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