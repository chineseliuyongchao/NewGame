namespace Game.Dialogue
{
    /// <summary>
    /// 为了在编辑器模式下运行时展示对话树的运行状态的接口
    /// </summary>
    public interface IGetDialogueTree
    {
        DialogueTree GetTree();
    }
}