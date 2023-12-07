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
            Town[] towns = JsonParser.ParseJson<Town>(textAsset.text);
            Dictionary<string, Town> townDictionary = this.GetModel<ITownModel>().TownData;
            foreach (Town town in towns)
            {
                townDictionary.TryAdd(town.Name, town);
            }
        }
    }
}