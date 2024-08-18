using System;
using System.Collections.Generic;
using Battle;
using Battle.Team;
using Game.GameSave;
using QFramework;
using UnityEngine;
using Object = System.Object;

namespace Game.GameBase
{
    public class GameModel : AbstractModel, IGameModel, ISaveModel
    {
        private GameTime _nowTime;
        private bool _timeIsPass;
        private PlayerTeam _playerTeam;
        private int _openStopTimeUITime;
        private Dictionary<int, LocalizationData> _localizationData;
        private Dictionary<int, LocalizationData> _dialogueLocalizationData;

        private Dictionary<int, LocalizationData> _dialogueTipLocalizationData;

        protected override void OnInit()
        {
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
            _localizationData = new Dictionary<int, LocalizationData>();
            _dialogueLocalizationData = new Dictionary<int, LocalizationData>();
            _dialogueTipLocalizationData = new Dictionary<int, LocalizationData>();
        }

        public GameTime NowTime
        {
            get => _nowTime;
            set => _nowTime = value;
        }

        public bool TimeIsPass
        {
            get => _timeIsPass && _openStopTimeUITime == 0; //时间可以流逝并且不能打开弹窗
            set => _timeIsPass = value;
        }

        public PlayerTeam PlayerTeam
        {
            get => _playerTeam;
            set => _playerTeam = value;
        }

        public int OpenStopTimeUITime
        {
            get => _openStopTimeUITime;
            set => _openStopTimeUITime = value;
        }

        public Dictionary<int, LocalizationData> LocalizationData
        {
            get => _localizationData;
            set => _localizationData = value;
        }

        public Dictionary<int, LocalizationData> DialogueLocalizationData
        {
            get => _dialogueLocalizationData;
            set => _dialogueLocalizationData = value;
        }

        public Dictionary<int, LocalizationData> DialogueTipLocalizationData
        {
            get => _dialogueTipLocalizationData;
            set => _dialogueTipLocalizationData = value;
        }

        public Object SaveModel()
        {
            return new GameModelData
            {
                year = _nowTime.Year,
                month = _nowTime.Month,
                day = _nowTime.Day,
                time = _nowTime.Time
            };
        }

        public void LoadModel(string data)
        {
            GameModelData gameModelData = JsonUtility.FromJson<GameModelData>(data);
            _nowTime = new GameTime(gameModelData.year, gameModelData.month, gameModelData.day, gameModelData.time);
        }

        public void InitializeModel()
        {
            _nowTime = new GameTime(GameTime.InitialTime);
        }

        public void NewArchiveInitData()
        {
            _timeIsPass = false;
            _openStopTimeUITime = 0;
        }

        public string ModelName()
        {
            return "GameData";
        }
    }

    [Serializable]
    public class GameModelData
    {
        public int year;
        public int month;
        public int day;
        public int time;
    }
}