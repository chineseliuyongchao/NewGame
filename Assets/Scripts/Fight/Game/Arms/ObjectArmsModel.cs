using System.Collections.Generic;
using Fight.Model;
using Fight.Utils;
using QFramework;
using UnityAttribute;
using UnityEngine;

namespace Fight.Game.Arms
{
    /**
     * 一个标准拥有近战、士气的兵种模型
     */
    public class ObjectArmsModel : AbstractModel, ICloneable<ObjectArmsModel>, ICanGetModel
    {
        /// <summary>
        /// 特质录入
        /// 需要根据自身的id进行排序方便先后计算
        /// </summary>
        public SortedSet<int> TraitSet = new();
        
        [Label("通用属性")] [SerializeField] private ArmData _armData;

        public ArmData ArmData
        {
            protected set => _armData = value;
            get => _armData;
        }
        
        /// <summary>
        /// 玩家自定义的兵种位置
        /// </summary>
        public int CurrentIndex;

        /// <summary>
        /// 通用的克隆方法
        /// </summary>
        /// <returns></returns>
        public virtual ObjectArmsModel Clone()
        {
            var result = (ObjectArmsModel)MemberwiseClone();
            result.TraitSet = new SortedSet<int>(TraitSet);
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