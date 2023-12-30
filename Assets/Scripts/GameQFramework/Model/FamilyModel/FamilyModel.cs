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
        private Dictionary<int, RoleData> _roleData;

        protected override void OnInit()
        {
            _familyCommonData = new Dictionary<int, FamilyCommonData>();
            _roleCommonData = new Dictionary<int, RoleCommonData>();

            _familyData = new Dictionary<int, FamilyData>();
            _roleData = new Dictionary<int, RoleData>();
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

        public Dictionary<int, RoleData> RoleData
        {
            get => _roleData;
            set => _roleData = value;
        }

        public object SaveModel()
        {
            FamilyModelData familyModelData = new FamilyModelData()
            {
                familyDataKey = new List<int>(_familyData.Keys),
                roleDataKey = new List<int>(_roleData.Keys)
            };
            for (int i = 0; i < _familyData.Count; i++)
            {
                familyModelData.familyDataValue.Add(_familyData[familyModelData.familyDataKey[i]]);
            }

            for (int i = 0; i < _roleData.Count; i++)
            {
                familyModelData.roleDataValue.Add(_roleData[familyModelData.roleDataKey[i]]);
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

            _roleData.Clear();
            for (int i = 0; i < familyModelData.roleDataKey.Count; i++)
            {
                _roleData.Add(familyModelData.roleDataKey[i], familyModelData.roleDataValue[i]);
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
                if (_familyData.ContainsKey(roleCommonData.FamilyId))
                {
                    _familyData[roleCommonData.FamilyId].familyRoleS.Add(roleCommonData.ID);
                }
            }

            _roleData.Clear();
            for (int i = 0; i < roleKey.Count; i++)
            {
                _roleData.Add(roleKey[i], new RoleData(_roleCommonData[roleKey[i]]));
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
        public List<int> roleDataKey = new();
        public List<RoleData> roleDataValue = new();
    }
}