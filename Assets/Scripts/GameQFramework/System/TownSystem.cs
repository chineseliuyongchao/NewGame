using System.Collections.Generic;
using Game.Town;
using QFramework;
using UnityEngine;
using Utils;

namespace GameQFramework
{
    public class TownSystem : AbstractSystem, ITownSystem
    {
        protected override void OnInit()
        {
        }

        public void InitTownData(TextAsset textAsset)
        {
            TownCommonData[] towns = JsonParser.ParseJson<TownCommonData>(textAsset.text);
            Dictionary<string, TownCommonData> townDictionary = this.GetModel<ITownModel>().TownCommonData;
            foreach (TownCommonData town in towns)
            {
                townDictionary.TryAdd(town.Name, town);
            }
        }
    }
}