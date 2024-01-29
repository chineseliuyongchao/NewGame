namespace Game.Dialogue
{
    /// <summary>
    /// 根节点
    /// </summary>
    public class RootDialogueNode : BaseDialogueNode
    {
        public override DialogueNodeType NodeType()
        {
            return DialogueNodeType.ROOT;
        }
    }
}