namespace Game.BehaviourTree
{
    /// <summary>
    /// 条件节点，用于判断是否满足某些条件
    /// </summary>
    public abstract class BaseConditionNode : BaseNode
    {
        protected override BehaviourTreeState OnUpdate()
        {
            return JudgeResult() ? BehaviourTreeState.SUCCESS : BehaviourTreeState.FAILURE;
        }

        /// <summary>
        /// 获取判断结果
        /// </summary>
        /// <returns></returns>
        protected abstract bool JudgeResult();
    }
}