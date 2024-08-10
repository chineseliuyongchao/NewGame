using System.Collections.Generic;
using QFramework;

namespace Fight.Model
{
    public class FightModel : AbstractModel, IFightModel
    {
        /// <summary>
        /// 当前战斗的所有单位数据
        /// </summary>
        private Dictionary<int, ArmData> _armDataTypes;

        protected override void OnInit()
        {
            _armDataTypes = new Dictionary<int, ArmData>();
        }

        public Dictionary<int, ArmData> ARMDataTypes
        {
            get => _armDataTypes;
            set => _armDataTypes = value;
        }
    }
}