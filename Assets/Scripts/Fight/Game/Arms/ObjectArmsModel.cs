using System.Collections.Generic;
using Fight.Game.Attribute;
using Fight.Utils;
using QFramework;
using UnityAttribute;
using UnityEngine;

namespace Fight.Game.Arms
{
    /**
     * 一个标准拥有近战、士气的兵种模型
     */
    public class ObjectArmsModel : AbstractModel, ICloneable<ObjectArmsModel>
    {
        [Label("通用属性")] [SerializeField] private ObjectAttribute _objAttribute = new();

        [Label("士气属性")] [SerializeField] private SpiritualAttribute _spiritualAttribute = new();

        /**
         * 特质需要根据自身的id进行排序方便先后计算
         */
        public SortedSet<int> TraitSet = new();

        public ObjectAttribute ObjectAttribute => _objAttribute;

        public RangedAttackAttribute RangedAttackAttribute => null;

        public SpiritualAttribute SpiritualAttribute => _spiritualAttribute;

        public virtual ObjectArmsModel Clone()
        {
            var result = (ObjectArmsModel)MemberwiseClone();
            result.TraitSet = new SortedSet<int>(TraitSet);
            result._objAttribute = (ObjectAttribute)_objAttribute.Clone();
            result._spiritualAttribute = (SpiritualAttribute)_spiritualAttribute.Clone();
            return result;
        }

        /**
         * 在这里可以进行读表初始化
         */
        protected override void OnInit()
        {
        }
    }
}