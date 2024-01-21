namespace Game.BehaviourTree
{
    /// <summary>
    /// 装饰者节点
    /// </summary>
    public abstract class BaseDecoratorNode : BaseNode
    {
        public BaseNode child;

        public override void Resetting()
        {
            base.Resetting();
            child.Resetting();
        }

        public override BaseNode Clone()
        {
            BaseDecoratorNode node = Instantiate(this);
            if (child != null)
            {
                node.child = child.Clone();
            }

            return node;
        }
    }
}