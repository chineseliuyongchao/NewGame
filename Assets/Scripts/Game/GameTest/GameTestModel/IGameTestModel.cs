using QFramework;

namespace Game.GameTest
{
    public interface IGameTestModel : IModel
    {
        /// <summary>
        /// 是否启用测试模式
        /// 只有 IsTest 为 true 时，其他测试功能才会生效
        /// </summary>
        bool IsTest { get; set; }

        /// <summary>
        /// 是否启用固定命中率
        /// 启用后，命中率将直接计算命中次数，而不是模拟真实命中率
        /// </summary>
        bool FixedHitRateEnabled { get; set; }

        /// <summary>
        /// 是否禁用 AI 攻击
        /// 启用后，AI 在判定要发动攻击时不会发动攻击
        /// </summary>
        bool AINoAttack { get; set; }

        /// <summary>
        /// 是否禁用 AI 移动
        /// 启用后，AI 在判定要移动时不会移动
        /// </summary>
        bool AINoMove { get; set; }

        /// <summary>
        /// 是否允许在战斗开始时摆放 AI
        /// 启用后，可以在战斗开始时自定义 AI 的位置
        /// </summary>
        bool CanPlaceAI { get; set; }

        /// <summary>
        /// 是否忽略作战意志计算
        /// 启用后，战斗中将不计算作战意志对战斗的影响
        /// </summary>
        bool IgnoreMorale { get; set; }

        /// <summary>
        /// 是否忽略疲劳值计算
        /// 启用后，战斗中将不计算疲劳值对战斗的影响
        /// </summary>
        bool IgnoreFatigue { get; set; }
    }
}