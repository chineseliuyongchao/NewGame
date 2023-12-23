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
                AccessTown = _accessTown
            };
        }

        public void LoadModel(string data)
        {
            MyPlayerModelData myPlayerModelData = JsonUtility.FromJson<MyPlayerModelData>(data);
            _accessTown = myPlayerModelData.AccessTown;
        }

        public void InitializeModel()
        {
            _accessTown = 0;
        }

        public string ModelName()
        {
            return GetType().ToString();
        }
    }

    public class MyPlayerModelData
    {
        public int AccessTown;
    }
}