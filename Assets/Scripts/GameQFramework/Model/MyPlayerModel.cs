using System;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class MyPlayerModel : AbstractModel, IMyPlayerModel, ISaveModel
    {
        private int _accessTown;

        protected override void OnInit()
        {
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public int AccessTown
        {
            get => _accessTown;
            set => _accessTown = value;
        }

        public object SaveModel()
        {
            return new MyPlayerModelData
            {
                accessTown = _accessTown
            };
        }

        public void LoadModel(string data)
        {
            MyPlayerModelData myPlayerModelData = JsonUtility.FromJson<MyPlayerModelData>(data);
            _accessTown = myPlayerModelData.accessTown;
        }

        public void InitializeModel()
        {
            _accessTown = 0;
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
    }
}