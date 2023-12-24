namespace GameQFramework
{
    public class YearChangeEvent
    {
    }

    public class MonthChangeEvent
    {
    }

    public class DayChangeEvent
    {
    }

    public class TimeChangeEvent
    {
    }

    public class QuarterChangeEvent
    {
    }

    /// <summary>
    /// 用于通知时间更新
    /// </summary>
    public class ChangeTimeEvent
    {
    }

    /// <summary>
    /// 切换到菜单场景
    /// </summary>
    public class ChangeToMenuSceneEvent
    {
    }

    /// <summary>
    /// 切换到主游戏场景
    /// </summary>
    public class ChangeToMainGameSceneEvent
    {
    }

    /// <summary>
    /// 保存了存档
    /// </summary>
    public class SaveFileDataEvent
    {
    }

    /// <summary>
    /// 设置时间是否可以流逝
    /// </summary>
    public class TimePassEvent
    {
        public bool IsPass;

        public TimePassEvent(bool isPass)
        {
            IsPass = isPass;
        }
    }
}