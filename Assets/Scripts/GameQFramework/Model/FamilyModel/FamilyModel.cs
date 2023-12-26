using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameQFramework.FamilyModel
{
    public class FamilyModel : AbstractModel, IFamilyModel, ISaveModel
    {
        private Dictionary<int, FamilyCommonData> _familyCommonData;
        private Dictionary<int, RoleCommonData> _roleCommonData;

        private Dictionary<int, FamilyData> _familyData;

        protected override void OnInit()
        {
            _familyCommonData = new Dictionary<int, FamilyCommonData>();
            _roleCommonData = new Dictionary<int, RoleCommonData>();

            _familyData = new Dictionary<int, FamilyData>();
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<int, FamilyCommonData> FamilyCommonData
        {
            get => _familyCommonData;
            set => _familyCommonData = value;
        }

        public Dictionary<int, RoleCommonData> RoleCommonData
        {
            get => _roleCommonData;
            set => _roleCommonData = value;
        }

        public Dictionary<int, FamilyData> FamilyData
        {
            get => _familyData;
            set => _familyData = value;
        }

        public object SaveModel()
        {
            FamilyModelData familyModelData = new FamilyModelData()
            {
                familyDataKey = new List<int>(_familyData.Keys)
            };
            for (int i = 0; i < _familyData.Count; i++)
            {
                familyModelData.familyDataValue.Add(_familyData[familyModelData.familyDataKey[i]]);
            }

            return familyModelData;
        }

        public void LoadModel(string data)
        {
            FamilyModelData familyModelData = JsonUtility.FromJson<FamilyModelData>(data);
            _familyData.Clear();
            for (int i = 0; i < familyModelData.familyDataKey.Count; i++)
            {
                _familyData.Add(familyModelData.familyDataKey[i], familyModelData.familyDataValue[i]);
            }
        }

        public void InitializeModel()
        {
            _familyData.Clear();
            List<int> familyKey = new List<int>(_familyCommonData.Keys);
            for (int i = 0; i < familyKey.Count; i++)
            {
                _familyData.Add(familyKey[i], new FamilyData(_familyCommonData[familyKey[i]]));
            }

            List<int> roleKey = new List<int>(_roleCommonData.Keys);
            for (int i = 0; i < roleKey.Count; i++)
            {
                RoleCommonData roleCommonData = _roleCommonData[roleKey[i]];
                _familyData[roleCommonData.FamilyId].familyRoleData.Add(new RoleData(roleCommonData));
            }
        }

        public string ModelName()
        {
            return "FamilyData";
        }
    }

    [Serializable]
    public class FamilyModelData
    {
        public List<int> familyDataKey = new();
        public List<FamilyData> familyDataValue = new();
    }
}