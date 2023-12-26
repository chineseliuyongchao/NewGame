using System.Collections.Generic;
using GameQFramework;
using QFramework;

namespace Game.Town
{
    /// <summary>
    /// 用于存放所有聚落的节点
    /// </summary>
    public class TownNode : BaseGameController
    {
        public BaseTown baseTown;
        private Dictionary<int, BaseTown> _towns;

        protected override void OnInit()
        {
            base.OnInit();
            _towns = new Dictionary<int, BaseTown>();
            InitTown();
        }

        /// <summary>
        /// 生成所有城市
        /// </summary>
        private void InitTown()
        {
            Dictionary<int, TownCommonData> townData = this.GetModel<ITownModel>().TownCommonData;
            foreach (var kvp in townData)
            {
                int key = kvp.Key;
                BaseTown town = Instantiate(baseTown, transform);
                town.InitTown(key);
                _towns.Add(key, town);
            }
        }
    }
}