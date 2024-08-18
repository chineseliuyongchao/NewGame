namespace Game.GameBase
{
    /// <summary>
    /// 用于通知时间更新
    /// </summary>
    public class ChangeTimeEvent
    {
    }

    /// <summary>
    /// 切换菜单场景
    /// </summary>
    public class ChangeMenuSceneEvent
    {
        public readonly bool IsChangeIn;

        public ChangeMenuSceneEvent(bool isChangeIn)
        {
            IsChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换主游戏场景
    /// </summary>
    public class ChangeBattleSceneEvent
    {
        public readonly bool IsChangeIn;

        public ChangeBattleSceneEvent(bool isChangeIn)
        {
            IsChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换游戏创建场景
    /// </summary>
    public class ChangeBattleCreateSceneEvent
    {
        public bool IsChangeIn;

        public ChangeBattleCreateSceneEvent(bool isChangeIn)
        {
            IsChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换战斗创建场景
    /// </summary>
    public class ChangeFightCreateSceneEvent
    {
        public bool IsChangeIn;

        public ChangeFightCreateSceneEvent(bool isChangeIn)
        {
            IsChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换战斗场景
    /// </summary>
    public class ChangeFightSceneEvent
    {
        public readonly bool IsChangeIn;

        public ChangeFightSceneEvent(bool isChangeIn)
        {
            IsChangeIn = isChangeIn;
        }
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
        public readonly bool IsPass;

        public TimePassEvent(bool isPass)
        {
            this.IsPass = isPass;
        }
    }

    /// <summary>
    /// 是否展示了弹窗
    /// </summary>
    public class ShowDialogEvent
    {
        public bool IsShow;

        public ShowDialogEvent(bool isShow)
        {
            this.IsShow = isShow;
        }
    }
}