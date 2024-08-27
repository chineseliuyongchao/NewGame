using QFramework;

namespace Battle.BattleBase
{
    /// <summary>
    /// 设置时间是否可以流逝
    /// </summary>
    public class TimePassCommand : AbstractCommand
    {
        private readonly bool _isPass;

        public TimePassCommand(bool isPass)
        {
            _isPass = isPass;
        }

        protected override void OnExecute()
        {
            this.GetModel<IBattleBaseModel>().TimeIsPass = _isPass;
            this.SendEvent(new TimePassEvent(_isPass));
        }
    }

    /// <summary>
    /// 是否展示了弹窗
    /// </summary>
    public class ShowDialogCommand : AbstractCommand
    {
        private readonly bool _isShow;

        public ShowDialogCommand(bool isShow)
        {
            _isShow = isShow;
        }

        protected override void OnExecute()
        {
            if (_isShow)
            {
                this.GetModel<IBattleBaseModel>().OpenStopTimeUITime++;
            }
            else
            {
                this.GetModel<IBattleBaseModel>().OpenStopTimeUITime--;
            }
            this.SendEvent(new ShowDialogEvent(_isShow));
        }
    }
}