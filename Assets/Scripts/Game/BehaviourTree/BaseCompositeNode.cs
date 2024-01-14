using System.Collections.Generic;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 复合节点
    /// </summary>
    public abstract class BaseCompositeNode : BaseNode
    {
        public List<BaseNode> children = new();

        public override BaseNode Clone()
        {
            BaseCompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }
    }
}