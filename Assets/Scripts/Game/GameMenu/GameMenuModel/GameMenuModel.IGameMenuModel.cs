using System.Collections.Generic;
using QFramework;

namespace Game.GameMenu
{
    public interface IGameMenuModel : IModel
    {
        public int Language { get; set; }

        public Dictionary<int, ArmDataType> ARMDataTypes { get; set; }
    }
}