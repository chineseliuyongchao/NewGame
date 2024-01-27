using System;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class MyPlayerModel : AbstractModel, IMyPlayerModel, ISaveModel
    {
        private int _accessTown;
        private CreateGameData _createGameData;
        private int _roleId;
        private int _teamId;

        protected override void OnInit()
        {
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public int AccessTown
        {
            get => _accessTown;
            set => _accessTown = value;
        }

        public CreateGameData CreateGameData
        {
            get => _createGameData;
            set => _createGameData = value;
        }

        public int RoleId
        {
            get => _roleId;
            set => _roleId = value;
        }

        public int TeamId
        {
            get => _teamId;
            set => _teamId = value;
        }

        public object SaveModel()
        {
            return new MyPlayerModelData
            {
                accessTown = _accessTown,
                roleId = _roleId
            };
        }

        public void LoadModel(string data)
        {
            MyPlayerModelData myPlayerModelData = JsonUtility.FromJson<MyPlayerModelData>(data);
            _accessTown = myPlayerModelData.accessTown;
            _roleId = myPlayerModelData.roleId;
        }

        public void InitializeModel()
        {
            _accessTown = 0;
            _roleId = 0;
        }

        public void NewArchiveInitData()
        {
        }

        public string ModelName()
        {
            return "MyPlayerData";
        }
    }

    [Serializable]
    public class MyPlayerModelData
    {
        public int accessTown;
        public int roleId;
    }

    public class CreateGameData
    {
        public string name;
        public int age;
    }
}