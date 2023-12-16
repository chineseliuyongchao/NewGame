using QFramework;

namespace GameQFramework
{
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