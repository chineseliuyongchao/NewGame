using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class TeamModel : AbstractModel, ITeamModel, ISaveModel
    {
        private Dictionary<int, TeamData> _teamData;

        protected override void OnInit()
        {
            _teamData = new Dictionary<int, TeamData>();
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public Dictionary<int, TeamData> TeamData
        {
            get => _teamData;
            set => _teamData = value;
        }

        public object SaveModel()
        {
            TeamModelData teamModelData = new TeamModelData
            {
                teamDataKey = new List<int>(_teamData.Keys)
            };
            for (int i = 0; i < _teamData.Count; i++)
            {
                teamModelData.teamDataValue.Add(_teamData[teamModelData.teamDataKey[i]]);
            }

            return teamModelData;
        }

        public void LoadModel(string data)
        {
            TeamModelData teamModelData = JsonUtility.FromJson<TeamModelData>(data);
            _teamData.Clear();
            for (int i = 0; i < teamModelData.teamDataKey.Count; i++)
            {
                _teamData.Add(teamModelData.teamDataKey[i], teamModelData.teamDataValue[i]);
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
            return "TeamData";
        }
    }

    [Serializable]
    public class TeamModelData
    {
        public List<int> teamDataKey = new();
        public List<TeamData> teamDataValue = new();
    }
}