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
        private Dictionary<string, BaseTown> _towns;

        protected override void OnInit()
        {
            base.OnInit();
            _towns = new Dictionary<string, BaseTown>();
            InitTown();
        }

        /// <summary>
        /// 生成所有城市
        /// </summary>
        private void InitTown()
        {
            Dictionary<string, Town> townData = this.GetModel<ITownModel>().TownData;
            foreach (var kvp in townData)
            {
                string key = kvp.Key;
                BaseTown town = Instantiate(baseTown, transform);
                town.InitTown(key);
                _towns.Add(key, town);
            }
        }
    }
}