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
}