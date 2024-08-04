using System;
using UnityAttribute;

namespace Fight.Game.Attribute
{
    [Serializable]
    public class ObjectAttribute : IAttribute
    {
        [Label("总血量")] public int totalBlood;
        [Label("当前血量")] public int currentBlood;
        [Label("总人数")] public int totalPeople;
        [Label("当前人数")] public int currentPeople;

        /// <summary>
        ///     攻击力
        /// </summary>
        [Label("攻击力")] public int attackPower;

        /// <summary>
        ///     冲锋加成
        /// </summary>
        [Label("冲锋加成")] public int chargePower;

        /// <summary>
        ///     防御力
        /// </summary>
        [Label("防御力")] public int defensePower;

        /// <summary>
        ///     普通杀伤
        /// </summary>
        [Label("普通杀伤")] public int normalDamage;

        /// <summary>
        ///     破甲杀伤
        /// </summary>
        [Label("破甲杀伤")] public int armorBreakDamage;

        /// <summary>
        ///     护甲强度
        /// </summary>
        [Label("护甲强度")] public int armorStrength;

        /// <summary>
        ///     移动力
        /// </summary>
        [Label("移动力")] public int movePower;

        /// <summary>
        ///     视野力
        /// </summary>
        [Label("视野力")] public int viewPower;

        /// <summary>
        ///     隐蔽力
        /// </summary>
        [Label("隐蔽力")] public int hiddenPower;

        /// <summary>
        ///     攻击范围
        /// </summary>
        [Label("攻击范围")] public int attackRange;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}