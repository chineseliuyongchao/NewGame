using QFramework;

namespace Game.GameMenu
{
    /// <summary>
    /// 删除存档
    /// </summary>
    public class DeleteFileCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new DeleteFileEvent());
        }
    }
}