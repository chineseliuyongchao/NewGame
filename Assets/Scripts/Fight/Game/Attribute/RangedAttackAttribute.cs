using System;
using UnityAttribute;

namespace Fight.Game.Attribute
{
    [Serializable]
    public class RangedAttackAttribute : IAttribute
    {
        /// <summary>
        ///     弹药量
        /// </summary>
        [Label("弹药量")] public int ammunition;

        /// <summary>
        ///     装填速度
        /// </summary>
        [Label("装填速度")] public int reloadSpeed;

        /// <summary>
        ///     射击精度
        /// </summary>
        [Label("射击精度")] public int accuracy;

        /// <summary>
        ///     普通杀伤
        /// </summary>
        [Label("普通杀伤")] public int normalDamage;

        /// <summary>
        ///     破甲杀伤
        /// </summary>
        [Label("破甲杀伤")] public int armorBreakDamage;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}