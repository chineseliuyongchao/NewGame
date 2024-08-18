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
        public readonly bool isChangeIn;

        public ChangeMenuSceneEvent(bool isChangeIn)
        {
            this.isChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换主游戏场景
    /// </summary>
    public class ChangeBattleSceneEvent
    {
        public readonly bool isChangeIn;

        public ChangeBattleSceneEvent(bool isChangeIn)
        {
            this.isChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换游戏创建场景
    /// </summary>
    public class ChangeBattleCreateSceneEvent
    {
        public bool isChangeIn;

        public ChangeBattleCreateSceneEvent(bool isChangeIn)
        {
            this.isChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换战斗创建场景
    /// </summary>
    public class ChangeFightCreateSceneEvent
    {
        public bool isChangeIn;

        public ChangeFightCreateSceneEvent(bool isChangeIn)
        {
            this.isChangeIn = isChangeIn;
        }
    }

    /// <summary>
    /// 切换战斗场景
    /// </summary>
    public class ChangeFightSceneEvent
    {
        public readonly bool isChangeIn;

        public ChangeFightSceneEvent(bool isChangeIn)
        {
            this.isChangeIn = isChangeIn;
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
}