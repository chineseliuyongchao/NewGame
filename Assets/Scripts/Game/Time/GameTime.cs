using System;
using Game.GameBase;

namespace Game
{
    /// <summary>
    /// 用于记录一个特定的时间
    /// </summary>
    public class GameTime
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Time { get; private set; }

        /// <summary>
        /// 无意义时间数据
        /// </summary>
        public const int NONE_TIME = -1;

        public GameTime(int year, int month, int day, int time)
        {
            Year = year;
            Month = month;
            Day = day;
            Time = time;
        }

        public GameTime(GameTime gameTime)
        {
            Year = gameTime.Year;
            Month = gameTime.Month;
            Day = gameTime.Day;
            Time = gameTime.Time;
        }

        /// <summary>
        /// 游戏初始时间
        /// </summary>
        public static readonly GameTime InitialTime = new(GameTimeConstant.INIT_YEAR, GameTimeConstant.INIT_MONTH,
            GameTimeConstant.INIT_DAY, GameTimeConstant.INIT_TIME);

        /// <summary>
        /// 刷新人口的时间
        /// </summary>
        public static readonly GameTime RefreshPopulationTime = new(-1, -1, -1, 0);

        /// <summary>
        /// 刷新民兵的时间
        /// </summary>
        public static readonly GameTime RefreshMilitiaTime = new(-1, -1, -1, 3);

        /// <summary>
        /// 刷新粮食的时间
        /// </summary>
        public static readonly GameTime RefreshGrainTime = new(-1, -1, -1, 8);

        /// <summary>
        /// 刷新聚落非存储数据的时间
        /// </summary>
        public static readonly GameTime RefreshTownNoStorageTime = new GameTime(-1, -1, -1, 8);

        /// <summary>
        /// 刷新经济的时间
        /// </summary>
        public static readonly GameTime RefreshEconomyTime = new GameTime(-1, -1, -1, 10);

        /// <summary>
        /// 更新时间
        /// </summary>
        public void UpdateTime()
        {
            Time++;
            if (Time >= GameTimeConstant.TIME_CONVERT_DAY)
            {
                Time = 0;
                Day++;
            }

            if (Day >= GameTimeConstant.DAY_CONVERT_MONTH)
            {
                Day = 0;
                Month++;
            }

            if (Month >= GameTimeConstant.MONTH_CONVERT_YEAR)
            {
                Month = 0;
                Year++;
            }
        }

        /// <summary>
        /// 获取一个随机的时间
        /// </summary>
        /// <param name="year">是否需要随机年，不需要就会设置无意义时间</param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static GameTime GetRandomTime(bool year = true, bool month = true, bool day = true, bool time = true)
        {
            Random random = new Random();
            int yearValue = NONE_TIME;
            if (year)
            {
                yearValue = random.Next(GameTimeConstant.INIT_YEAR, GameTimeConstant.INIT_YEAR + 1000);
            }

            int monthValue = NONE_TIME;
            if (month)
            {
                monthValue = random.Next(GameTimeConstant.MONTH_CONVERT_YEAR);
            }

            int dayValue = NONE_TIME;
            if (day)
            {
                dayValue = random.Next(GameTimeConstant.DAY_CONVERT_MONTH);
            }

            int timeValue = NONE_TIME;
            if (time)
            {
                timeValue = random.Next(GameTimeConstant.TIME_CONVERT_DAY);
            }

            return new GameTime(yearValue, monthValue, dayValue, timeValue);
        }

        /// <summary>
        /// 获取时间ID
        /// </summary>
        /// <returns></returns>
        public int TimeID()
        {
            return (Year - InitialTime.Year) * GameTimeConstant.MONTH_CONVERT_YEAR +
                   (Month - InitialTime.Month) * GameTimeConstant.DAY_CONVERT_MONTH +
                   (Day - InitialTime.Day) * GameTimeConstant.TIME_CONVERT_DAY + (Time - InitialTime.Time);
        }

        public bool Equals(GameTime other)
        {
            // Check if any of the parameters equals to NONE_TIME
            bool yearEquals = Year == NONE_TIME || other.Year == NONE_TIME || Year == other.Year;
            bool monthEquals = Month == NONE_TIME || other.Month == NONE_TIME || Month == other.Month;
            bool dayEquals = Day == NONE_TIME || other.Day == NONE_TIME || Day == other.Day;
            bool timeEquals = Time == NONE_TIME || other.Time == NONE_TIME || Time == other.Time;

            // Return true if all parameters are either NONE_TIME or equal
            return yearEquals && monthEquals && dayEquals && timeEquals;
        }


        // 重载 ToString 方法，方便输出时间信息
        public override string ToString()
        {
            return $"{Year}-{Month}-{Day} {Time}:00";
        }
    }
}