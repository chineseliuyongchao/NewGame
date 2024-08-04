using Fight.Game.Attribute;
using UnityAttribute;
using UnityEngine;

namespace Fight.Game.Arms
{
    /**
     * 一个标准拥有近战、远程、士气的兵种模型
     */
    public class RangedAttackArmsModel : ObjectArmsModel
    {
        [Label("远程属性")] [SerializeField] private RangedAttackAttribute _rangedAttackAttribute;

        public new RangedAttackAttribute RangedAttackAttribute => _rangedAttackAttribute;

        public new RangedAttackArmsModel Clone()
        {
            var result = (RangedAttackArmsModel)base.Clone();
            result._rangedAttackAttribute = (RangedAttackAttribute)_rangedAttackAttribute.Clone();
            return result;
        }
    }
}