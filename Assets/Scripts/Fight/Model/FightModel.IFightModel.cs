using System.Collections.Generic;
using QFramework;

namespace Fight.Model
{
    public interface IFightModel : IModel
    {
        public Dictionary<int, ArmData> ARMDataTypes { get; set; }
    }
}