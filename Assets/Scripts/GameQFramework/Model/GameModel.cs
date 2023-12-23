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

        public Object SaveModel()
        {
            return new GameModelData
            {
                Year = _year,
                Month = _month,
                Day = _day,
                Time = _time,
                Quarter = _quarter
            };
        }

        public void LoadModel(string data)
        {
            GameModelData gameModelData = JsonUtility.FromJson<GameModelData>(data);
            _year = gameModelData.Year;
            _month = gameModelData.Month;
            _day = gameModelData.Day;
            _time = gameModelData.Time;
            _quarter = gameModelData.Quarter;
        }

        public void InitializeModel()
        {
            _year = GameTimeConstant.INIT_YEAR;
            _month = GameTimeConstant.INIT_MONTH;
            _day = GameTimeConstant.INIT_DAY;
            _time = GameTimeConstant.INIT_TIME;
            _quarter = GameTimeConstant.INIT_QUARTER;
        }

        public string ModelName()
        {
            return GetType().ToString();
        }
    }

    public class GameModelData
    {
        public int Year;
        public int Month;
        public int Day;
        public int Time;
        public int Quarter;
    }
}