namespace Battle.BattleBase
{
    /// <summary>
    /// 设置时间是否可以流逝
    /// </summary>
    public class TimePassEvent
    {
        public readonly bool isPass;

        public TimePassEvent(bool isPass)
        {
            this.isPass = isPass;
        }
    }

    /// <summary>
    /// 是否展示了弹窗
    /// </summary>
    public class ShowDialogEvent
    {
        public bool isShow;

        public ShowDialogEvent(bool isShow)
        {
            this.isShow = isShow;
        }
    }
}