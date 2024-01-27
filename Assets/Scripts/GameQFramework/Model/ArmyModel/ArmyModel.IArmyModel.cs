using System.Collections.Generic;
using QFramework;

namespace GameQFramework
{
    public interface IArmyModel : IModel
    {
        /// <summary>
        /// 军队数据
        /// </summary>
        public Dictionary<int, ArmyData> ArmyData { get; set; }
    }
}