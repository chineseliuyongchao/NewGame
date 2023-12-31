﻿using QFramework;

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
    public class HasShowDialogCommand : AbstractCommand
    {
        private readonly bool _hasShow;

        public HasShowDialogCommand(bool hasShow)
        {
            _hasShow = hasShow;
        }

        protected override void OnExecute()
        {
            this.GetModel<IGameModel>().HasShowDialog = _hasShow;
            this.SendEvent(new HasShowDialogEvent(_hasShow));
        }
    }
}