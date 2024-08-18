using System;
using System.Collections.Generic;
using Battle.Family;
using Battle.Town;
using Game.GameSave;
using QFramework;
using UnityEngine;

namespace Battle.Country
{
    public class CountryModel : AbstractModel, ICountryModel, ISaveModel
    {
        private Dictionary<int, CountryCommonData> _countryCommonData;
        private Dictionary<int, CountryNameData> _countryNameData;
        private Dictionary<int, CountryData> _countryData;

        protected override void OnInit()
        {
            _countryCommonData = new Dictionary<int, CountryCommonData>();
            _countryNameData = new Dictionary<int, CountryNameData>();
            _countryData = new Dictionary<int, CountryData>();

            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<int, CountryCommonData> CountryCommonData
        {
            get => _countryCommonData;
            set => _countryCommonData = value;
        }

        public Dictionary<int, CountryNameData> CountryNameData
        {
            get => _countryNameData;
            set => _countryNameData = value;
        }

        public Dictionary<int, CountryData> CountryData
        {
            get => _countryData;
            set => _countryData = value;
        }

        public object SaveModel()
        {
            CountryModelData countryModelData = new CountryModelData()
            {
                countryDataKey = new List<int>(_countryData.Keys)
            };
            for (int i = 0; i < _countryData.Count; i++)
            {
                countryModelData.countryDataValue.Add(_countryData[countryModelData.countryDataKey[i]]);
            }

            return countryModelData;
        }

        public void LoadModel(string data)
        {
            CountryModelData countryModelData = JsonUtility.FromJson<CountryModelData>(data);
            _countryData.Clear();
            for (int i = 0; i < countryModelData.countryDataKey.Count; i++)
            {
                _countryData.Add(countryModelData.countryDataKey[i], countryModelData.countryDataValue[i]);
            }
        }

        public void InitializeModel()
        {
            _countryData.Clear();
            List<int> countryKey = new List<int>(_countryCommonData.Keys);
            for (int i = 0; i < countryKey.Count; i++)
            {
                _countryData.Add(countryKey[i],
                    new CountryData(_countryCommonData[countryKey[i]], _countryNameData[countryKey[i]]));
            }

            Dictionary<int, FamilyCommonData> familyCommonDataS = this.GetModel<IFamilyModel>().FamilyCommonData;
            List<int> familyKey = new List<int>(familyCommonDataS.Keys);
            for (int i = 0; i < familyKey.Count; i++)
            {
                FamilyCommonData familyCommonData = familyCommonDataS[familyKey[i]];
                _countryData[familyCommonData.CountryId].countryFamilyS.Add(familyCommonData.ID);
            }

            Dictionary<int, TownCommonData> townCommonDataS = this.GetModel<ITownModel>().TownCommonData;
            List<int> townKey = new List<int>(townCommonDataS.Keys);
            for (int i = 0; i < townKey.Count; i++)
            {
                TownCommonData townCommonData = townCommonDataS[townKey[i]];
                _countryData[townCommonData.CountryId].countryTownS.Add(townCommonData.ID);
            }
        }

        public void NewArchiveInitData()
        {
        }

        public string ModelName()
        {
            return "CountryData";
        }
    }

    [Serializable]
    public class CountryModelData
    {
        public List<int> countryDataKey = new();
        public List<CountryData> countryDataValue = new();
    }
}