﻿namespace Game.GameBase
{
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
    /// 切换到游戏创建场景
    /// </summary>
    public class ChangeToGameCreateSceneEvent
    {
    }

    /// <summary>
    /// 保存存档之前发送，用于将数据更新到Model中
    /// </summary>
    public class SoonSaveFileEvent
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