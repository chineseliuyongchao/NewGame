using System;
using Game.Team;
using QFramework;
using UnityEngine;
using Utils.Constant;
using Object = System.Object;

namespace GameQFramework
{
    public class GameModel : AbstractModel, IGameModel, ISaveModel
    {
        private int _year;
        private int _month;
        private int _day;
        private int _time;
        private int _quarter;
        private bool _timeIsPass;
        private PlayerTeam _playerTeam;
        private int _openStopTimeUITime;

        protected override void OnInit()
        {
            this.GetSystem<IGameSaveSystem>().AddSaveModel(this);
        }

        public int Year
        {
            get => _year;
            set
            {
                _year = value;
                this.SendEvent(new YearChangeEvent());
            }
        }

        public int Month
        {
            get => _month;
            set
            {
                _month = value;
                this.SendEvent(new MonthChangeEvent());
            }
        }

        public int Day
        {
            get => _day;
            set
            {
                _day = value;
                this.SendEvent(new DayChangeEvent());
            }
        }

        public int Time
        {
            get => _time;
            set
            {
                _time = value;
                this.SendEvent(new TimeChangeEvent());
            }
        }

        public int Quarter
        {
            get => _quarter;
            set
            {
                _quarter = value;
                this.SendEvent(new QuarterChangeEvent());
            }
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

        public Object SaveModel()
        {
            return new GameModelData
            {
                year = _year,
                month = _month,
                day = _day,
                time = _time,
                quarter = _quarter
            };
        }

        public void LoadModel(string data)
        {
            GameModelData gameModelData = JsonUtility.FromJson<GameModelData>(data);
            _year = gameModelData.year;
            _month = gameModelData.month;
            _day = gameModelData.day;
            _time = gameModelData.time;
            _quarter = gameModelData.quarter;
        }

        public void InitializeModel()
        {
            _year = GameTimeConstant.INIT_YEAR;
            _month = GameTimeConstant.INIT_MONTH;
            _day = GameTimeConstant.INIT_DAY;
            _time = GameTimeConstant.INIT_TIME;
            _quarter = GameTimeConstant.INIT_QUARTER;
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
        public int quarter;
    }
}