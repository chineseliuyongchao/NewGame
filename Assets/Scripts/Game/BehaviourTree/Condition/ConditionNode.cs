namespace Game.BehaviourTree
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

        public override string GetDescription()
        {
            return "这是一个测试条件节点是否有效的临时节点";
        }

        protected override bool JudgeResult()
        {
            return result;
        }
    }
}