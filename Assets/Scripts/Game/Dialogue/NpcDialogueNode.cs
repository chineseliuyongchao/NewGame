namespace Game.Dialogue
{
    /// <summary>
    /// npc节点
    /// </summary>
    public class NpcDialogueNode : BaseDialogueNode
    {
        public override DialogueNodeType NodeType()
        {
            return DialogueNodeType.NPC;
        }
    }
}