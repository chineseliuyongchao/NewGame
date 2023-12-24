using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class TownModel : AbstractModel, ITownModel, ISaveModel
    {
        private Dictionary<string, TownCommonData> _townCommonData;
        private Dictionary<string, TownData> _townData;

        protected override void OnInit()
        {
            _townCommonData = new Dictionary<string, TownCommonData>();
            _townData = new Dictionary<string, TownData>();
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<string, TownCommonData> TownCommonData
        {
            get => _townCommonData;
            set => _townCommonData = value;
        }

        public Dictionary<string, TownData> TownData
        {
            get => _townData;
            set => _townData = value;
        }

        public object SaveModel()
        {
            TownModelData townModelData = new TownModelData
            {
                townDataKey = new List<string>(_townData.Keys)
            };
            for (int i = 0; i < _townData.Count; i++)
            {
                townModelData.townDataValue.Add(_townData[townModelData.townDataKey[i]]);
            }

            return townModelData;
        }

        public void LoadModel(string data)
        {
            TownModelData townModelData = JsonUtility.FromJson<TownModelData>(data);
            _townData.Clear();
            for (int i = 0; i < townModelData.townDataKey.Count; i++)
            {
                _townData.Add(townModelData.townDataKey[i], townModelData.townDataValue[i]);
            }
        }

        public void InitializeModel()
        {
            _townData.Clear();
            List<string> key = new List<string>(_townCommonData.Keys);
            for (int i = 0; i < key.Count; i++)
            {
                _townData.Add(key[i], new TownData(_townCommonData[key[i]]));
            }
        }

        public string ModelName()
        {
            return "TownData";
        }
    }

    [Serializable]
    public class TownModelData
    {
        public List<string> townDataKey = new();
        public List<TownData> townDataValue = new();
    }
}