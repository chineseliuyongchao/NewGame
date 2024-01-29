namespace Game.Dialogue
{
    /// <summary>
    /// 玩家节点
    /// </summary>
    public class PlayerDialogueNode : BaseDialogueNode
    {
        public override DialogueNodeType NodeType()
        {
            return DialogueNodeType.PLAYER;
        }
    }
}