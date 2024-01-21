namespace Game.BehaviourTree.Condition
{
    /// <summary>
    /// 判断是否可以组建新军队的节点
    /// </summary>
    public class CanBuildArmyNode : BaseConditionNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        public override string GetDescription()
        {
            return "用于判断是否可以组建新的军队";
        }

        protected override bool JudgeResult()
        {
            return aiAgent.CanBuildArmy();
        }
    }
}