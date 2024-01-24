namespace Game.BehaviourTree
{
    /// <summary>
    /// 移动到聚落的节点
    /// </summary>
    public class MoveToTownNode : BaseActionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            aiAgent.MoveToTown();
            return BehaviourTreeState.SUCCESS;
        }

        public override string GetDescription()
        {
            return "移动到聚落";
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