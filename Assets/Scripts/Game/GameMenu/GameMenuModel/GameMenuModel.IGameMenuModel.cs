using System.Collections.Generic;
using QFramework;

namespace Game.GameMenu
{
    public interface IGameMenuModel : IModel
    {
        public int Language { get; set; }

        /// <summary>
        /// 所有兵种通用数据
        /// </summary>
        public Dictionary<int, ArmDataType> ARMDataTypes { get; set; }
    }
}