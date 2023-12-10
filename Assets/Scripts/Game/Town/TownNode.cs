using System.Collections.Generic;
using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.Town
{
    /// <summary>
    /// 用于存放所有聚落的节点
    /// </summary>
    public class TownNode : BaseGameController
    {
        public TextAsset textAsset;
        public BaseTown baseTown;
        private Dictionary<string, BaseTown> _towns;

        protected override void OnInit()
        {
            base.OnInit();
            this.GetSystem<ITownSystem>().InitTownData(textAsset);
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
                string key = kvp.Key; // 获取键
                Town value = kvp.Value; // 获取值
                BaseTown town = Instantiate(baseTown, transform);
                town.gameObject.transform.position = new Vector3(value.TownPos[0], value.TownPos[1], 0);
                _towns.Add(key, town);
            }
        }
    }
}