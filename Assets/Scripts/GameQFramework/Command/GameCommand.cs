using QFramework;

namespace GameQFramework
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
    /// 切换到菜单场景
    /// </summary>
    public class ChangeToMenuSceneCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new ChangeToMenuSceneEvent());
        }
    }

    /// <summary>
    /// 切换到主游戏场景
    /// </summary>
    public class ChangeToMainGameSceneCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent(new ChangeToMainGameSceneEvent());
        }
    }
}