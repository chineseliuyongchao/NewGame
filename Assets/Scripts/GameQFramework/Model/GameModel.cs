using QFramework;

namespace GameQFramework
{
    public class GameModel : AbstractModel, IGameModel
    {
        private int _year = 1;

        private int _month = 1;

        private int _day = 1;

        private int _time = 1;

        private int _quarter = 1;

        protected override void OnInit()
        {
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
    }
}