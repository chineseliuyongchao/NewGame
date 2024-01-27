namespace Game.BehaviourTree
{
    /// <summary>
    /// 判断是否前往聚落
    /// </summary>
    public class CanMoveToBuildNode : BaseConditionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        public override string GetDescription()
        {
            return "用于判断是否前往聚落";
        }

        protected override bool JudgeResult()
        {
            bool res = aiAgent.CanMoveToTown(out int townId);
            if (res)
            {
                blackboard.targetTownId = townId;
            }

            return res;
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