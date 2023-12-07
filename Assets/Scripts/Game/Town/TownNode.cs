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

        protected override void OnInit()
        {
            base.OnInit();
            this.GetSystem<ITownSystem>().InitTownData(textAsset);
        }
    }
}