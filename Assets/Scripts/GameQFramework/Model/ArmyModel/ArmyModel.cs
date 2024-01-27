using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class ArmyModel : AbstractModel, IArmyModel, ISaveModel
    {
        private Dictionary<int, ArmyData> _armyData;

        protected override void OnInit()
        {
            _armyData = new Dictionary<int, ArmyData>();
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<int, ArmyData> ArmyData
        {
            get => _armyData;
            set => _armyData = value;
        }

        public object SaveModel()
        {
            ArmyModelData armyModelData = new ArmyModelData
            {
                armyDataKey = new List<int>(_armyData.Keys)
            };
            for (int i = 0; i < _armyData.Count; i++)
            {
                armyModelData.armyDataValue.Add(_armyData[armyModelData.armyDataKey[i]]);
            }

            return armyModelData;
        }

        public void LoadModel(string data)
        {
            ArmyModelData armyModelData = JsonUtility.FromJson<ArmyModelData>(data);
            _armyData.Clear();
            for (int i = 0; i < armyModelData.armyDataKey.Count; i++)
            {
                _armyData.Add(armyModelData.armyDataKey[i], armyModelData.armyDataValue[i]);
            }
        }

        public void InitializeModel()
        {
        }

        public void NewArchiveInitData()
        {
        }

        public string ModelName()
        {
            return "ArmyData";
        }
    }

    [Serializable]
    public class ArmyModelData
    {
        public List<int> armyDataKey = new();
        public List<ArmyData> armyDataValue = new();
    }
}