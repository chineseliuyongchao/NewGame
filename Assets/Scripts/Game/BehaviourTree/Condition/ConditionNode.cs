namespace Game.BehaviourTree.Condition
{
    /// <summary>
    /// 测试条件节点
    /// </summary>
    public class ConditionNode : BaseConditionNode
    {
        public bool result;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override bool JudgeResult()
        {
            return result;
        }
    }
}