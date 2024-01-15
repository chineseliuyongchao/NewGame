namespace Game.BehaviourTree
{
    /// <summary>
    /// 为了在编辑器模式下运行时展示行为树的运行状态的接口
    /// </summary>
    public interface IGetBehaviourTree
    {
        /// <summary>
        /// 获取行为树
        /// </summary>
        /// <returns></returns>
        BehaviourTree GetTree();
    }
}