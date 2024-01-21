using System.Collections.Generic;
using GameQFramework;
using QFramework;
using Utils.Constant;

namespace Game.Town
{
    /// <summary>
    /// 用于存放所有聚落的节点
    /// </summary>
    public class TownNode : BaseGameController
    {
        private BaseTown _townPrefab;
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
            _townPrefab = resLoader.LoadSync<Town>(GamePrefabConstant.TOWN);
            Dictionary<int, TownCommonData> townData = this.GetModel<ITownModel>().TownCommonData;
            foreach (var kvp in townData)
            {
                int key = kvp.Key;
                BaseTown town = Instantiate(_townPrefab, transform);
                town.InitTown(key);
                _towns.Add(key, town);
            }
        }
    }
}