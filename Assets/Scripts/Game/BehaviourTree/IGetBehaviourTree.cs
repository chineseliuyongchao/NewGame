namespace Game.BehaviourTree
{
    public interface IGetBehaviourTree
    {
        /// <summary>
        /// 获取行为树
        /// </summary>
        /// <returns></returns>
        BehaviourTree GetTree();
    }
}