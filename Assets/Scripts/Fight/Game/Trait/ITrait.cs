using Fight.Game.Arms;

namespace Fight.Game.Trait
{
    public interface ITrait
    {
        int Id { get; }

        /// <summary>
        ///     添加该特质时触发
        /// </summary>
        /// <param name="attributes">当前兵种实例</param>
        void Apply(ObjectArmsModel attributes);

        /// <summary>
        ///     移除该特质时触发
        /// </summary>
        /// <param name="attributes">当前兵种实例</param>
        void Remove(ObjectArmsModel attributes);

        /// <summary>
        ///     当开始判定该兵种的特质范围开始影响时触发
        ///     一般在兵种移动后调用
        /// </summary>
        /// <param name="owner">当前兵种实例</param>
        void StartAffecting(ObjectArmsModel owner)
        {
        }

        /// <summary>
        ///     当开始判定该兵种的特质范围结束影响时触发
        ///     一般在兵种即将移动后调用
        /// </summary>
        /// <param name="owner">当前兵种实例</param>
        void StopAffecting(ObjectArmsModel owner)
        {
        }

        /// <summary>
        ///     开始该兵种回合时调用，刷新状态
        /// </summary>
        /// <param name="owner">当前兵种实例</param>
        void StartRound(ObjectArmsModel owner)
        {
        }

        /// <summary>
        ///     结束该兵种回合时调用，刷新状态
        /// </summary>
        /// <param name="owner">当前兵种实例</param>
        void EndRound(ObjectArmsModel owner)
        {
        }
    }
}