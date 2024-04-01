using QFramework;

namespace Game.GameBase
{
    /// <summary>
    /// 用于通知时间更新
    /// </summary>
    public class ChangeTimeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new ChangeTimeEvent());
        }
    }

    /// <summary>
    /// 保存存档之前发送，用于将数据更新到Model中
    /// </summary>
    public class SoonSaveFileCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new SoonSaveFileEvent());
        }
    }

    /// <summary>
    /// 保存了存档
    /// </summary>
    public class SaveFileDataCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new SaveFileDataEvent());
        }
    }

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
            this.GetModel<IGameModel>().TimeIsPass = _isPass;
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
                this.GetModel<IGameModel>().OpenStopTimeUITime++;
            }
            else
            {
                this.GetModel<IGameModel>().OpenStopTimeUITime--;
            }
            this.SendEvent(new ShowDialogEvent(_isShow));
        }
    }
}