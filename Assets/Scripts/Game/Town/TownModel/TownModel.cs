using System;
using System.Collections.Generic;
using Game.Family;
using Game.GameSave;
using QFramework;
using UnityEngine;

namespace Game.Town
{
    public class TownModel : AbstractModel, ITownModel, ISaveModel
    {
        private Dictionary<int, TownCommonData> _townCommonData;
        private Dictionary<int, TownData> _townData;

        protected override void OnInit()
        {
            _townCommonData = new Dictionary<int, TownCommonData>();
            _townData = new Dictionary<int, TownData>();
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<int, TownCommonData> TownCommonData
        {
            get => _townCommonData;
            set => _townCommonData = value;
        }

        public Dictionary<int, TownData> TownData
        {
            get => _townData;
            set => _townData = value;
        }

        public object SaveModel()
        {
            TownModelData townModelData = new TownModelData
            {
                townDataKey = new List<int>(_townData.Keys)
            };
            for (int i = 0; i < _townData.Count; i++)
            {
                townModelData.townDataValue.Add(_townData[townModelData.townDataKey[i]].storage);
            }

            return townModelData;
        }

        public void LoadModel(string data)
        {
            TownModelData townModelData = JsonUtility.FromJson<TownModelData>(data);
            _townData.Clear();
            for (int i = 0; i < townModelData.townDataKey.Count; i++)
            {
                _townData.Add(townModelData.townDataKey[i], new TownData(townModelData.townDataValue[i]));
            }
        }

        public void InitializeModel()
        {
            _townData.Clear();
            List<int> key = new List<int>(_townCommonData.Keys);
            for (int i = 0; i < key.Count; i++)
            {
                _townData.Add(key[i], new TownData(new TownDataStorage(_townCommonData[key[i]])));
            }

            Dictionary<int, RoleCommonData> roleCommonDataS = this.GetModel<IFamilyModel>().RoleCommonData;
            List<int> roleKey = new List<int>(roleCommonDataS.Keys);
            for (int i = 0; i < roleKey.Count; i++)
            {
                RoleCommonData roleCommonData = roleCommonDataS[roleKey[i]];
                _townData[roleCommonData.TownId].storage.townRoleS.Add(roleCommonData.ID);
            }
        }

        public void NewArchiveInitData()
        {
        }

        public string ModelName()
        {
            return "TownData";
        }
    }

    [Serializable]
    public class TownModelData
    {
        public List<int> townDataKey = new();
        public List<TownDataStorage> townDataValue = new();
    }
}