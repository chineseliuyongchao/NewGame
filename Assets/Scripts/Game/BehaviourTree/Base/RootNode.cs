namespace Game.BehaviourTree
{
    /// <summary>
    /// 根节点
    /// </summary>
    public class RootNode : BaseNode
    {
        public BaseNode child;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            return child.Update();
        }

        public override void Resetting()
        {
            base.Resetting();
            if (child != null)
            {
                child.Resetting();
            }
        }

        public override BaseNode Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        public override string GetDescription()
        {
            return "每棵行为树的根节点";
        }
    }
}